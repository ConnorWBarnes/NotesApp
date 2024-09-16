import NoteGrid from "@/components/notes/note-grid";
import { noteService } from "@/services";

export default async function Page() {
  let notes = await noteService.getNotesAsync();
  return <NoteGrid notes={notes}/>;
}
