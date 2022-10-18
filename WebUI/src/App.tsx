import React from 'react';
import { BrowserRouter as Router, Navigate, Route, Routes } from 'react-router-dom';
import Core from './views/core';
import Public from './views/public';

class App extends React.Component {
  render() {
    return (
      <Router>
        <Routes>
          <Route path="emails/*" element={<Core />} />
          <Route path="/" element={<Navigate to="emails" />} />
          <Route path="public/*" element={<Public />} />
          <Route path="*" element={<Public />} />
        </Routes>
      </Router>
    );
  }
}

export default App;
