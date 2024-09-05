import { getArchivedNotesAsync } from "@/app/lib/note-service";
import NoteGrid from "@/app/ui/notes/note-grid";

export default async function Page() {
  let notes = await getArchivedNotesAsync();
  return <NoteGrid notes={notes}/>;
}
