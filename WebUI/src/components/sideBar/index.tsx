import EmailIcon from '@mui/icons-material/Email';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import InfoIcon from '@mui/icons-material/Info';
import { Drawer, List, ListItem, ListItemIcon, ListItemText } from '@mui/material';

const Sidebar = (): JSX.Element => {
  return (
    <Drawer variant="permanent">
      <List>
        <ListItem button>
          <ListItemIcon>
            <AccountCircleIcon />
          </ListItemIcon>
          <ListItemText primary="Emails" />
        </ListItem>
        <ListItem button>
          <ListItemIcon>
            <EmailIcon />
          </ListItemIcon>
          <ListItemText primary="Profile" />
        </ListItem>
        <ListItem button>
          <ListItemIcon>
            <InfoIcon />
          </ListItemIcon>
          <ListItemText primary="About reader service" />
        </ListItem>
      </List>
    </Drawer>
  );
};

export default Sidebar;
