using UnityEngine;
using System.Collections;

namespace Makezi.StateMachine {

	public abstract class State<T> {

		protected StateMachine<T> stateMachine;		// Reference to state machine this state belongs to
		protected T context;						// Reference to context of state machine

		/*
		Constructor
		*/
		public State(){}

		/*
		Constructor
		*/
		public State(StateMachine<T> stateMachine, T context){
			Init(stateMachine, context);
		}

		/* Initialises which state machine and context this state belongs to */
		public void Init(StateMachine<T> stateMachine, T context){
			this.stateMachine = stateMachine;
			this.context = context;
		}

		/* State initialization (setting values, etc.) */
		public abstract void Enter();

		/* Handles state work and checking conditions for state transitions (called within Mono Update) */
		public abstract void Update();

		/* Handles state work and checking conditions for state transitions (called within Mono FixedUpdate) */
		public abstract void FixedUpdate();
	}
}
