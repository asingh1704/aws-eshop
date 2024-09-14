import axios from 'axios';
export const exchangeCodeForToken = async (code) => {
    try {
        const params = new URLSearchParams();
        params.append('grant_type', 'authorization_code');
        params.append('client_id', '7am8optki35ia5ftachv574me5');
        params.append('redirect_uri', 'http://localhost:31090');
        params.append('code', code);

        const response = await axios.post('https://eshoponcloud.auth.us-east-1.amazoncognito.com/oauth2/token', params.toString(), {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
        });

        //setAccessToken(response.data.access_token);
        console.log('mf got the Access Token:', response.data);
        const { id_token, access_token, refresh_token } = response.data;
    
            // Store tokens in localStorage (or sessionStorage)
            localStorage.setItem('id_token', id_token);
            localStorage.setItem('access_token', access_token);
            localStorage.setItem('refresh_token', refresh_token);
        return true;
        // Here you can save the token in localStorage or state for further use
    } catch (error) {
        console.error('Error exchanging code for token:', error.response ? error.response.data : error.message);
    }
    return false;
};

export const getCatalog = async () => {
    try {
      const response = await axios.get(`http://localhost:5248/catalog`, {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('access_token')}`,
        }
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching catalog data:', error);
      throw error;
    }
  };