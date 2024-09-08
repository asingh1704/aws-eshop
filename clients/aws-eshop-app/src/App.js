import React,{useEffect, useState} from 'react';
import { BrowserRouter as Router, Route, Routes, useNavigate } from 'react-router-dom';
import { exchangeCodeForToken } from './services/authservice';
import Home from './components/Home';
import UnknwnUser from './components/UnknowsUser';


function App() {
  const navigate = useNavigate();

  useEffect(()=>{
    const AuthenticateThisMF = async () => {
      const idtoken = localStorage.getItem('id_token');
      if(!idtoken)
      {
        console.log("user not authenticated");
        const urlParams = new URLSearchParams(window.location.search);
        const authCode = urlParams.get('code');
        if(!authCode)
        {
          console.log("no auth code too");
          const loginUrl = 'https://eshoponcloud.auth.us-east-1.amazoncognito.com/oauth2/authorize?response_type=code&client_id=7am8optki35ia5ftachv574me5&redirect_uri=http://localhost:3000';
          window.location.href = loginUrl;
        }
        else
        {
          console.log("hmm atleast authcode is there");
          var isLoginSuccess = await exchangeCodeForToken(authCode);
          if(isLoginSuccess){
          navigate('/');}
          else{
            navigate('/unknwnuser');
          }
        }
      }
      else{
        console.log("user is authenticated");
      }
    }

    AuthenticateThisMF();
  },[]);
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/unknwnuser" element={<UnknwnUser />} />
    </Routes>
  );
}

export default App;
