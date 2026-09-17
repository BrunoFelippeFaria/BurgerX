import { Box, Typography } from "@mui/material";
import type { ReactNode } from "react";

export default function LoginLayout({ children }: { children: ReactNode }) {
  return (
    <Box sx={{ display: "flex", minHeight: "100vh" }}>
      <Box
        sx={{
          width: { xs: "100%", md: "45%" },
          display: "flex",
          flexDirection: "column",
          justifyContent: "center",
          alignItems: "center",
          px: { xs: 3, md: 8 },
        }}
      >
        <Box sx={{ width: "100%", maxWidth: 360 }}>
          <Typography align="center" variant="h4" gutterBottom>
            🍔 BurgerX
          </Typography>
          {children}
        </Box>
      </Box>
    </Box>
  );
}
