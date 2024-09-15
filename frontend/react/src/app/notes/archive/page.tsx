import { getArchivedNotesAsync } from "@/services/note-service";
import NoteGrid from "@/components/notes/note-grid";

export default async function Page() {
  let notes = await getArchivedNotesAsync();
  return <NoteGrid notes={notes}/>;
}
