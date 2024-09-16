'use client';

import { useActionState } from "react";
import { loginActionAsync } from '@/actions/auth-actions';

// TODO: Add validation and error handling
export function LoginForm() {
  const [state, action] = useActionState(loginActionAsync, undefined);
  //const [errorMessage, formAction, isPending] = useActionState(authenticateActionAsync, undefined);

  return (
    <div className=" align-middle container-fluid gx-0 min-vh-100">
      <div className="row align-items-center min-vh-100">
        <div className="col-md-10 mx-auto col-lg-5">
          <form action={action} className="p-4 p-md-5 border rounded-3 bg-body-tertiary">
            {/* Email input */}
            <div className="input-group has-validation">
              <div className="form-floating mb-3 w-full">
                <input
                  className="form-control"
                  id="email"
                  type="email"
                  name="email"
                  placeholder="Enter your email address"
                  required
                />
                <label htmlFor="email">
                  Email address
                </label>
              </div>
            </div>

            {/* Password input */}
            <div className="input-group has-validation">
              <div className="form-floating mb-3 w-full">
                <input
                  className="form-control"
                  id="password"
                  type="password"
                  name="password"
                  placeholder="Enter password"
                  required
                  minLength={6}
                />
                <label htmlFor="password">
                  Password
                </label>
              </div>
            </div>
            {/*TODO: Add disabled={isPending} aria-disabled={isPending}*/}
            <button className="btn btn-lg mt-4 w-full" type="submit">
              Log in
            </button>
            <div className="mt-4">
              <div className="flex h-8 items-end space-x-1" aria-live="polite" aria-atomic="true">
                {/*TODO*/}
                {/*{errorMessage && (*/}
                {/*  <>*/}
                {/*    <p className="text-sm text-red-500">{errorMessage}</p>*/}
                {/*  </>*/}
                {/*)}*/}
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
