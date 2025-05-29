-- Step 1: Delete all existing data
DELETE FROM RoleButtons;

-- Step 2: Reset identity seed to 0 (so next insert starts at 1)
DBCC CHECKIDENT ('RoleButtons', RESEED, 0);

-- Step 3: Insert the new record
INSERT INTO RoleButtons (RoleID, ButtonName, LinkedFormName)
VALUES (1, 'Menu', 'MenuForm');
