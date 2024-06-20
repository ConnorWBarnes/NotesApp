import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, RedirectCommand, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';

import { Note } from './note';
import { NoteService } from './note.service';

export const noteResolver: ResolveFn<Note> = async (
  route: ActivatedRouteSnapshot, 
  state: RouterStateSnapshot
) => {
  const router = inject(Router);
  const noteService = inject(NoteService);
  const id = route.paramMap.get('id')!;

  try {
    return await noteService.getNoteAsync(id);
  } catch {
    return new RedirectCommand(router.parseUrl('/404'));
  }
}
