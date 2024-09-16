'use server';

import { authService } from "@/services";
import { LoginFormState, LoginFormSchema, RegisterUserFormState, RegisterUserFormSchema } from "@/types/definitions";
import { deleteAuthToken, setAuthTokenAsync } from "@/utils/auth-utils";

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
    const authToken = await authService.loginAsync(validatedFields.data.email, validatedFields.data.password);
    if (authToken) {
      await setAuthTokenAsync(authToken);
    }
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
  deleteAuthToken();
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
  await authService.registerUserAsync(validatedFields.data.email, validatedFields.data.password);
}
