import { useActionState } from "react";
import { signOutActionAsync } from "@/actions/auth-actions";
import { auth, signOut } from "../../../auth";

export default async function SignOutComponent() {
  const session = await auth();

  return (
    <>
      <div>{`Hello ${session?.user?.name}`}</div>
      <form action={signOutActionAsync}>
        <button type="submit">Sign out</button>
      </form>
    </>
  );
}
