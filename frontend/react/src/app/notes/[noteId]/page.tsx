import { getNoteAsync } from "@/lib/note-service";
import NoteEditForm from "@/components/notes/note-edit-form";

export default async function Page({ params }: { params: { noteId: string } }) {
  let note = await getNoteAsync(params.noteId);
  return <NoteEditForm note={note}/>;
}
