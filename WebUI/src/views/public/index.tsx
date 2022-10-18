import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';

// views
import LogIn from './logIn';
import NotFound from './notFound';

class Public extends React.Component {
  render() {
    return (
      <Routes>
        <Route path="/" element={<Navigate to="login" />} />
        <Route path="login" element={<LogIn />} />
        <Route path="not-found" element={<NotFound />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    );
  }
}

export default Public;
