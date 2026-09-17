import { Button, IconButton, InputAdornment, Stack, TextField, Typography } from "@mui/material";
import LoginLayout from "./LoginLayout";

import Visibility from "@mui/icons-material/Visibility";
import VisibilityOff from "@mui/icons-material/VisibilityOff";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { authService } from "../../features/auth/services/authService";

export default function LoginPage() {
  const [userName, setUserName] = useState("");
  const [userPass, setUserPass] = useState("");
  const [showPassword, setShowPassword] = useState(false);

  const navigate = useNavigate();

  async function Login() {
    const response = await authService.login({
      userName: userName,
      password: userPass,
    });

    if (response instanceof Error) {
      console.log(response.message);
      return;
    }

    navigate("/home");
  }

  return (
    <LoginLayout>
      <Typography variant="h5" gutterBottom>
        Login
      </Typography>
      <Stack spacing={2}>
        <TextField
          label="Usuário"
          value={userName}
          onChange={(e) => setUserName(e.target.value)}
        />
        <TextField
          label="Senha"
          type={showPassword ? "text" : "password"}
          value={userPass}
          onChange={(e) => setUserPass(e.target.value)}
          slotProps={{
            input: {
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton onClick={() => setShowPassword(!showPassword)}>
                    {showPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              ),
            },
          }}
        />
        <Button onClick={() => Login()} variant="contained">
          Entrar
        </Button>
      </Stack>
    </LoginLayout>
  );
}