import type { NextAuthConfig } from 'next-auth';

export const authConfig = {
  debug: process.env.NODE_ENV !== 'production',
  pages: {
    signIn: '/login',
  },
  providers: [], // Added later in auth.ts since it requires bcrypt which is only compatible with Node.js while this file is also used in non-Node.js environments
  callbacks: {
    authorized({ auth, request: { nextUrl } }) {
      const isLoggedIn = !!auth?.user;
      const isOnNotesPage = nextUrl.pathname.startsWith('/notes');
      if (isOnNotesPage) {
        if (isLoggedIn) return true;
        return false; // Redirect unauthenticated users to login page
      } else if (isLoggedIn) {
        return Response.redirect(new URL('/notes', nextUrl));
      }
      return true;
    },
  },
} satisfies NextAuthConfig;
