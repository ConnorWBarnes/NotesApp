import React from "react";
import CustomLayout from "@/components/custom-layout";

export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <CustomLayout>
      {children}
    </CustomLayout>
  );
}
