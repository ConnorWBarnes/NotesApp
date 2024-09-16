import { authService } from "@/services";
import { cookies } from "next/headers";

export const useLogin = () => {
  const loginAsync = async (email: string, password: string) => {
    const token = await authService.loginAsync(email, password);
    if (token) {
      cookies().set('token', JSON.stringify(token));
    }

    return token;
  };

  return { loginAsync };
};
