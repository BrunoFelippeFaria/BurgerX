import { Button, TextField } from "@mui/material";
import { useState } from "react";
import { authService } from "../../features/auth/services/authService";
import { useNavigate } from "react-router-dom";

export default function LoginPage() {
    const [userName, setUserName] = useState(''); 
    const [userPass, setUserPass] = useState(''); 
    
    const navigate = useNavigate()

    async function Login() {
        const response = await authService.login({
            userName: userName,
            password: userPass
        })

        if (response instanceof(Error)) {
            console.log(response.message)
            return
        }
        
        navigate('/home')
    }

    return (
        <>
            <h1>Login</h1>
            <TextField
                label="Usuário"
                value={userName}
                onChange={e => setUserName(e.target.value)}
            />

            <TextField
                label="Senha"
                value={userPass}
                onChange={e => setUserPass(e.target.value)}
            />
            
            <br />
            
            <Button onClick={() => Login()}>Entrar</Button>
        </>
    )
}