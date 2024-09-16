'use server';

import { revalidatePath } from "next/cache";
import { redirect } from "next/navigation";
import { noteService } from "@/services";
import { Note } from "@/types/note";

export async function createNoteActionAsync(emptyNote: Note, formData: FormData) {
  let note: Note = {
    id: '',
    title: formData.get("title") as string,
    body: formData.get("body") as string,
    isArchived: false,
  };

  note.id = await noteService.createNoteAsync(note);

  await navigateToNotesAsync();
  return note;
}

export async function updateNoteActionAsync(note: Note, formData: FormData) {
  const updatedNote: Note = {
    id: note.id,
    title: formData.get("title") as string,
    body: formData.get("body") as string,
    isArchived: note.isArchived,
  };

  if (note.title !== updatedNote.title || note.body !== updatedNote.body) {
    await noteService.updateNoteAsync(updatedNote);
  }

  await navigateToNotesAsync();
  return updatedNote;
}

export async function archiveNoteActionAsync(note: Note, formData: FormData) {
  const updatedNote: Note = {
    id: note.id,
    title: formData.get("title") as string,
    body: formData.get("body") as string,
    isArchived: !note.isArchived,
  };

  await noteService.updateNoteAsync(updatedNote);

  await navigateToNotesAsync();
  return updatedNote;
}

/**
 * Navigates the user back to the notes page they came from (i.e. /notes or /notes/archive).
 */
export async function navigateToNotesAsync() {
  // TODO: Navigate to /notes/archive when appropriate
  const notesUrl = '/notes';
  revalidatePath(notesUrl);
  redirect(notesUrl);
}
