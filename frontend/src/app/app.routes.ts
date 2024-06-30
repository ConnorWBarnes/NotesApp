import { Routes } from '@angular/router';

import { archivedNotesResolver, noteResolver, notesResolver } from './note/note.resolver';
import { NoteCreateComponent } from './note/note-create/note-create.component';
import { NoteEditComponent } from './note/note-edit/note-edit.component';
import { NoteGridComponent } from './note/note-grid/note-grid.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

export const routes: Routes = [
  {
    path: 'create',
    pathMatch: 'full',
    component: NoteCreateComponent
  },
  {
    path: 'notes/:id',
    component: NoteEditComponent,
    resolve: { note: noteResolver }
  },
  {
    path: 'notes',
    pathMatch: 'full',
    component: NoteGridComponent,
    resolve: { notes: notesResolver }
  },
  {
    path: 'archive',
    pathMatch: 'full',
    component: NoteGridComponent,
    resolve: { notes: archivedNotesResolver }
  },
  // Cannot redirect to a redirect because the router handles redirects once at each level
  { path: '', redirectTo: '/notes', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent },
];
