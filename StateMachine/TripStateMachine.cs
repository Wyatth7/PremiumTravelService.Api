﻿using PremiumTravelService.Api.Persistence.Entities.StateMachine;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Services.DataStorage;
using PremiumTravelService.Api.StateMachine.Factory;
using PremiumTravelService.Api.StateMachine.States;

namespace PremiumTravelService.Api.StateMachine;

public class TripStateMachine
{
    private readonly IDataStorageService _dataStorageService;
    
    private TripState TripState { get; set; }

    private IState _currentState;

    public TripStateMachine(IDataStorageService dataStorageService)
    {
        _dataStorageService = dataStorageService;
    }
    
    /// <summary>
    /// Creates the start of a trip state
    /// </summary>
    public async Task CreateState()
    {
        TripState = new()
        {
            TripId = Guid.NewGuid(),
            CurrentState = StateType.Create,
            IsComplete = false,
        };
        
        _currentState = StateFactory.CreateStateInstance(StateType.Create);

        await HandleProcess(TripState.TripId.ToString(), true, true);
    }
    
    /// <summary>
    /// Resumes trip state machine, or notifies if state is completed
    /// </summary>
    /// <param name="tripId">Trip Id to resume</param>
    /// <returns>Itinerary if completed, true if complete </returns>
    public async Task<(Itinerary, bool)> ResumeState(Guid tripId, object payload)
    {
        // get state
        var storageData = await _dataStorageService.Read();

        var stateMachine = storageData.StateMachines
            .FirstOrDefault(sm => sm.TripId == tripId);
        
        var trip = storageData.Trips
            .FirstOrDefault(trip => trip.TripId == tripId);
        
        // if complete, return complete state
        if (stateMachine is null || trip is null)
            throw new NullReferenceException("The trip requested does not exist.");

        // load current state
        if (stateMachine.IsComplete) return (new(), true);

        TripState = stateMachine;
        _currentState = StateFactory.CreateStateInstance(TripState.CurrentState);

        await HandleProcess(payload);
        
        return (null, false);
    }

    /// <summary>
    /// Goes to next state
    /// </summary>
    public void NextState()
    {
        // if completed, return
        if (TripState.CurrentState == StateType.Itinerary)
        {
            TripState.IsComplete = true;
            return;
        };
        
        // move to next state
        TripState.CurrentState = (StateType) ((int) TripState.CurrentState + 1);
    }
    
    /// <summary>
    /// Handles the process of the current state
    /// </summary>
    /// <param name="payload">Data to send to process</param>
    /// <param name="isNew">Is trip new?</param>
    /// <param name="shouldGoNext">Should next be called after process completes</param>
    /// <typeparam name="TPayload">Type of payload</typeparam>
    /// <exception cref="NullReferenceException">Null exception</exception>
    private async Task HandleProcess<TPayload>(TPayload payload, bool isNew = false, bool shouldGoNext = false)
    {
        if (payload is null) throw new NullReferenceException("A state payload cannot be null.");
        
        var storageData = await _dataStorageService.Read();

        var queryTripData = storageData.Trips
            .FirstOrDefault(trip => trip.TripId == TripState.TripId);
        
        if (!isNew)
        {
            if (queryTripData is null) 
                throw new NullReferenceException("This trip does not exist.");
        }
        
        var trip = _currentState.Process(queryTripData, payload);

        if (shouldGoNext)
        {
            NextState();
        }
        
        await SaveTripState(trip);
    }

    /// <summary>
    /// Saves trip data and state machine data
    /// </summary>
    /// <param name="trip">Trip to save</param>
    private async Task SaveTripState(Trip trip)
    {
        var storageData = await _dataStorageService.Read();

        if (storageData.Trips.Any(oldTrip => oldTrip.TripId == trip.TripId)
            && storageData.StateMachines.Any(sm => sm.TripId == trip.TripId))
        {
            storageData.Trips.Remove(storageData.Trips
                .Single(oldTrip => oldTrip.TripId == trip.TripId));
            
            storageData.StateMachines.Remove(storageData.StateMachines.Single(sm => sm.TripId == trip.TripId));
        }
        
        storageData.Trips.Add(trip);

        storageData.StateMachines.Add(TripState);

        await _dataStorageService.Write(storageData);
    }
}