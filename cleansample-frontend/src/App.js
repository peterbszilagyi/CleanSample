import Header from "./components/Header";
import Airplanes from "./components/Airplanes"
import AddAirplane from "./components/AddAirplane"
import { useState, useEffect } from 'react'

function App() {

  const [showAddAirplane, setShowAddAirplane] = useState(false)
  const [airplanes, setAirplanes] = useState([]);



  useEffect(() => { getAirplanes() }, []);

  const getAirplanes = () => {
    fetch("http://localhost:61993/api/airplane/getall")
      .then(res => res.json())
      .then(
        (result) => {
          setAirplanes(result);
        },
        (error) => {
          alert('Cannot reach server, error:', error);
        }
      );
  }


  const addAirplane = (name) => {
    const requestOptions = {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(name)
    };
    fetch('http://localhost:61993/api/airplane', requestOptions)
      .then(response => {
        getAirplanes();
      });
  }

  const modifyAirplane = (airplane) => {
    const requestOptions = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(airplane)
    };
    fetch('http://localhost:61993/api/airplane', requestOptions)
      .then(response => {
        getAirplanes();
      });
  }

  const deleteAirplane = (id) => {
    const requestOptions = {
      method: 'DELETE',
      headers: { 'Content-Type': 'application/json' }
    };
    fetch('http://localhost:61993/api/airplane/' + id, requestOptions)
      .then(response => {
        getAirplanes();
      });;
  }


  return (
    <div className="container">
      <Header
        title='Airplanes'
        onAdd={() => setShowAddAirplane(!showAddAirplane)}
        showAdd={showAddAirplane} />
      {showAddAirplane &&
        <AddAirplane
          onAdd={addAirplane}
          onModify={modifyAirplane} />}
      {airplanes.length > 0
        ?
        (<Airplanes
          airplanes={airplanes}
          onDelete={deleteAirplane}
          onModify={modifyAirplane} />)
        :
        ('No airplanes')}
    </div>
  );
}



export default App;
