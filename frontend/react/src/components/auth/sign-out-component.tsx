import { signOutActionAsync } from "@/actions/auth-actions";
import { useCurrentUser } from "@/hooks/auth/useCurrentUser";

export default async function SignOutComponent() {
  const currentUser = useCurrentUser();

  return (
    <>
      <div>{`Hello ${currentUser?.name}`}</div>
      <form action={signOutActionAsync}>
        <button type="submit">Sign out</button>
      </form>
    </>
  );
}
