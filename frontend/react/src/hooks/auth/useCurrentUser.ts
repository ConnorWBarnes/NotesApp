import { User } from "@/types/user";
import { getUserToken } from "@/utils/auth-utils";
import { useEffect, useState } from "react";

export const useCurrentUser = () => {
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const currentUser = getUserToken();
    if (currentUser) {
      setUser(currentUser);
    }
  }, []);

  return user;
};
