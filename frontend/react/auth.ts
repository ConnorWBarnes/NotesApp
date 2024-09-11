import { AuthToken } from "@/app/lib/auth/auth-token";
import NextAuth from 'next-auth';
import Credentials from 'next-auth/providers/credentials';
import { authConfig } from './auth.config';
import { getUserAsync, loginAsync } from "@/app/lib/auth/auth-service";
import { z } from 'zod';

export const { auth, signIn, signOut } = NextAuth({
  ...authConfig,
  providers: [
    Credentials({
      async authorize(credentials) {
        const parsedCredentials = z
          .object({ email: z.string().email(), password: z.string().min(6) })
          .safeParse(credentials);

        if (parsedCredentials.success) {
          const { email, password } = parsedCredentials.data;
          const authToken = await loginAsync(email, password) as AuthToken;
          if (authToken) {
            return await getUserAsync(authToken.accessToken);
          }
        }

        return null;
      },
    })
  ],
});
