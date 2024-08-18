import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-note-compose-base',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './note-compose-base.component.html',
  styleUrl: './note-compose-base.component.scss'
})
export abstract class NoteComposeBaseComponent {
  noteTitle = '';
  noteBody = '';
  
  abstract saveButtonText: string;

  abstract save(): void;
}
