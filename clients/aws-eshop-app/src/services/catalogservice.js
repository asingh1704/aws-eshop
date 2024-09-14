import axios from 'axios';
export const getCatalog = async () => {
    try {
      const url = process.env.REACT_APP_API_BASE_URL + '/catalog/products'
      const response = await axios.get(`url`, {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('access_token')}`,
        }
      });
      console.log(response.data)
      return response.data;
    } catch (error) {
      console.error('Error fetching catalog data:', error);
      throw error;
    }
  };