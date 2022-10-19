import { Button } from '@mui/material';

function EmailsList() {
  return (
    <div>
      Emails list
      <br />
      <Button variant="contained">Primary</Button>
      <br />
      <Button color="secondary" variant="contained">
        secondary
      </Button>
      <br />
      <Button color="error" variant="contained">
        Error
      </Button>
      <br />
      <Button color="success" variant="contained">
        Success
      </Button>
    </div>
  );
}

export default EmailsList;
