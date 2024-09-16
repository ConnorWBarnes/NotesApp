import { NextRequest, NextResponse } from "next/server";
import { loginRoute, profileRoute, protectedRoutes } from "@/routes/routes";
import { AuthToken } from "@/types/auth-token";
import { authTokenKey } from "@/utils/auth-utils";

export function middleware(request: NextRequest) {
  const authToken: AuthToken = request.cookies.has(authTokenKey) ? JSON.parse(request.cookies.get(authTokenKey)!.value) : undefined;

  // Check if the user is attempting to access a protected route
  if (protectedRoutes.some((protectedRoute) => request.nextUrl.pathname.startsWith(protectedRoute))) {
    // Redirect the user to the login page if they are not authenticated
    if (!authToken) {
      return NextResponse.redirect(new URL(loginRoute, request.url));
    }
  }

  // Check if the user is navigating to the login page even though they are already authenticated
  if (request.nextUrl.pathname.startsWith(loginRoute) && authToken) {
    return NextResponse.redirect(new URL(profileRoute, request.url));
  }
}
