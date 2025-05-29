-- Delete all existing data
DELETE FROM RoleButtons;

-- Insert the new record
INSERT INTO RoleButtons (RoleID, ButtonName, LinkedFormName)
VALUES (1, 'Menu', 'MenuForm');
