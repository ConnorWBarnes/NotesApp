'use server';

import { loginAsync, registerUserAsync } from "@/app/lib/auth/auth-service";
import { LoginFormState, LoginFormSchema, RegisterUserFormState, RegisterUserFormSchema } from "@/app/lib/definitions";
import { AuthError } from "next-auth";
import { signIn, signOut } from "../../../auth";

export async function authenticateActionAsync(prevState: string | undefined, formData: FormData) {
  try {
    await signIn('credentials', formData);
  } catch (error) {
    if (error instanceof AuthError) {
      switch (error.type) {
        case 'CredentialsSignin':
          return 'Invalid credentials.';
        default:
          return 'Something went wrong.';
      }
    }
    throw error;
  }
}

export async function loginActionAsync(state: LoginFormState, formData: FormData): Promise<LoginFormState> {
  // Validate form fields
  const validatedFields = LoginFormSchema.safeParse({
    email: formData.get('email'),
    password: formData.get('password'),
  })

  // If any form fields are invalid, return early
  if (!validatedFields.success) {
    return {
      errors: validatedFields.error.flatten().fieldErrors,
    };
  }

  // Call the provider or db to create a user...
  try {
    await loginAsync(validatedFields.data.email, validatedFields.data.password);
  } catch (error) {
    if (error instanceof Error) {
      return {
        message: error.message,
      };
    }
    throw error;
  }
}

export async function signOutActionAsync(formData: FormData): Promise<void> {
  await signOut();
}

export async function registerUserActionAsync(state: RegisterUserFormState, formData: FormData): Promise<RegisterUserFormState> {
  // Validate form fields
  const validatedFields = RegisterUserFormSchema.safeParse({
    name: formData.get('name'),
    email: formData.get('email'),
    password: formData.get('password'),
  })

  // If any form fields are invalid, return early
  if (!validatedFields.success) {
    return {
      errors: validatedFields.error.flatten().fieldErrors,
    }
  }

  // Call the provider or db to create a user...
  await registerUserAsync(validatedFields.data.email, validatedFields.data.password);
}
