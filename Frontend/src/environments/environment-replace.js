const fs = require('fs');
const path = require('path');
const dotenv = require('dotenv');

dotenv.config();

const envFiles = [
    path.join(__dirname, '../environments/environment.ts'),
    path.join(__dirname, '../environments/environment.development.ts'),
    path.join(__dirname, '../environments/environment.production.ts')
];

envFiles.forEach((filePath) => {
    let envFileContent = fs.readFileSync(filePath, 'utf-8');

    envFileContent = envFileContent.replace('${CLIENT_ID}', process.env.CLIENT_ID);

    fs.writeFileSync(filePath, envFileContent);
});