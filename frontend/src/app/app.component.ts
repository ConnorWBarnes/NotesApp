import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { NoteGridComponent } from './note/note-grid/note-grid.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    NoteGridComponent,
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Notes App';
}
