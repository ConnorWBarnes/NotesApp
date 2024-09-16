import { deleteAuthToken } from "@/utils/auth-utils";

export const useLogout = () => {
  const logout = () => {
    deleteAuthToken();
  };

  return { logout };
};
