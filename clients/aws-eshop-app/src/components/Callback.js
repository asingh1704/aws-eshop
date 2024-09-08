import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const Callback = () => {
    useEffect(() => {
        const urlParams = new URLSearchParams(window.location.search);
        const authCode = urlParams.get('code');

        if (authCode) {
            exchangeCodeForTokens(authCode);
          } else {
            console.error('No authorization code found in the URL.');
          }
    }, []);

    const exchangeCodeForTokens = (code) => {
        const tokenUrl = `https://eshoponcloud.auth.us-east-1.amazoncognito.com/oauth2/oauth2/token`;
    
        const params = new URLSearchParams({
          grant_type: 'authorization_code',
          client_id: CLIENT_ID,
          client_secret: CLIENT_SECRET,
          redirect_uri: REDIRECT_URI,
          code,
        });
    
        axios
          .post(tokenUrl, params, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
          })
          .then((response) => {
            console.log("got the mf token " +  response.data);
            const { id_token, access_token, refresh_token } = response.data;
    
            // Store tokens in localStorage (or sessionStorage)
            localStorage.setItem('id_token', id_token);
            localStorage.setItem('access_token', access_token);
            localStorage.setItem('refresh_token', refresh_token);
    
            // Redirect to home or dashboard after successful login
            navigate('/');
          })
          .catch((error) => {
            console.error('Error exchanging code for tokens:', error);
          });
      };
    
      return (
        <div>
          <h1>Completing login...</h1>
        </div>
      );
    };
    
export default Callback;