import NoteEditForm from "@/components/notes/note-edit-form";
import { noteService } from "@/services";

export default async function Page({ params }: { params: { noteId: string } }) {
  let note = await noteService.getNoteAsync(params.noteId);
  return <NoteEditForm note={note}/>;
}
