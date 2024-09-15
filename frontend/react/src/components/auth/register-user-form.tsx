'use client';

import { useActionState } from "react";
import { useFormStatus } from 'react-dom';
import { registerUserActionAsync } from '@/actions/auth-actions';

// TODO: Add validation and error handling
export function RegisterUserForm() {
  const [state, action] = useActionState(registerUserActionAsync, undefined);

  return (
    <form action={action}>
      <div>
        <label htmlFor="name">Name</label>
        <input id="name" name="name" placeholder="Name" />
      </div>
      {state?.errors?.name && <p>{state.errors.name}</p>}

      <div>
        <label htmlFor="email">Email</label>
        <input id="email" name="email" type="email" placeholder="Email" />
      </div>
      {state?.errors?.email && <p>{state.errors.email}</p>}

      <div>
        <label htmlFor="password">Password</label>
        <input id="password" name="password" type="password" />
      </div>
      {state?.errors?.password && (
        <div>
          <p>Password must:</p>
          <ul>
            {/*{state.errors.password.map((error) => (*/}
            {/*  <li key={error}>- {error}</li>*/}
            {/*))}*/}
          </ul>
        </div>
      )}
      <SubmitButton/>
    </form>
  );
}

function SubmitButton() {
  const { pending } = useFormStatus()

  return (
    <button disabled={pending} type="submit">
      Sign Up
    </button>
  )
}
