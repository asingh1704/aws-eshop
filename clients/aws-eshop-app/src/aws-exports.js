const awsconfig = {
    Auth: {
        region: 'us-east-1',
        userPoolId: 'us-east-1_tdDYq6hv5', // Replace with your User Pool ID
        userPoolWebClientId: '7am8optki35ia5ftachv574me5', // Replace with your App Client ID
        oauth: {
            domain: 'eshoponcloud.auth.us-east-1.amazoncognito.com',
            scope: ['openid', 'profile', 'email'],
            redirectSignIn: 'http://localhost:3000/', // Replace with your app's URL
            redirectSignOut: 'http://localhost:3000/', // Replace with your app's URL
            responseType: 'code',
        },loginWith: {
            email: true,
          }
    }
  };
  
  export default awsconfig;
  