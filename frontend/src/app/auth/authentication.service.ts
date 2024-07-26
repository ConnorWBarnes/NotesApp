import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { firstValueFrom, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { Response } from './response';
import { UserClaim } from './user-claim';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  // TODO: Refactor url, headers, and HttpClient into base class?
  private readonly authenticationUrl = 'http://localhost:3000/auth';

  private readonly httpHeaders = new HttpHeaders(
    {
      'Content-Type': 'application/json',
    }
  );

  constructor(private http: HttpClient) { }

  // Stores the URL that requires authentication so that we can redirect to it after successful authentication.
  private _redirectUrl?: string;
  public get redirectUrl(): string {
    return this._redirectUrl ?? '/';
  }
  public set redirectUrl(url: string | undefined) {
    this._redirectUrl = url;
  }

  private _isLoggedIn = false;
  get isLoggedIn(): boolean {
    return this._isLoggedIn;
  }

  public signIn$(email: string, password: string, rememberMe: boolean): Observable<Response> {
    return this.http.post<Response>(this.authenticationUrl, { email: email, password: password, rememberMe: rememberMe }, { headers: this.httpHeaders }).pipe(
      tap((response: Response) => this._isLoggedIn = response.isSuccess)
    );
  }

  public async signInAsync(email: string, password: string, rememberMe: boolean): Promise<Response> {
    const response = await firstValueFrom(this.http.post<Response>(this.authenticationUrl, { email: email, password: password, rememberMe: rememberMe }, { headers: this.httpHeaders }));
    this._isLoggedIn = response.isSuccess;
    return response;
  }

  public signOut$(): Observable<Response> {
    return this.http.delete<Response>(this.authenticationUrl).pipe(
      tap(_ => this._isLoggedIn = false)
    );
  }

  public async signOutAsync(): Promise<Response> {
    const response = await firstValueFrom(this.http.delete<Response>(this.authenticationUrl));
    this._isLoggedIn = false;
    return response;
  }

  public getUser$(): Observable<UserClaim[]> {
    return this.http.get<UserClaim[]>(this.authenticationUrl);
  }

  public async getUserAsync(): Promise<UserClaim[]> {
    return await firstValueFrom(this.http.get<UserClaim[]>(this.authenticationUrl));
  }
}
