import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import './App.css'
import Layout from '../Layout/Layout';
import ErrorPage from '../ErrorPage/ErrorPage';
import Home from '../Home/Home';
import TranslatorsList from '../../TranslatorManagement/TranslatorsList/TranslatorsList';

//Navigation table is rendered in Layout component
const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    errorElement: <ErrorPage />,
    children: [
      { 
        index: true, 
        element: <Home /> ,
      },
      {
        path: "translators",
        element: <TranslatorsList />,
      },
    ],
  },
]);

const App = () => {
  return (
    <div>
      <RouterProvider router={router} />
    </div>
  )
}

export default App;
