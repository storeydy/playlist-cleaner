const fs = require('fs');
const path = require('path');
const dotenv = require('dotenv');

dotenv.config();


const envFiles = [
    path.join(__dirname, 'environment.ts'),
    path.join(__dirname, 'environment.development.ts'),
    path.join(__dirname, 'environment.production.ts')
];

envFiles.forEach((filePath) => {
    let envFileContent = fs.readFileSync(filePath, 'utf-8');

    envFileContent = envFileContent.replace('${CLIENT_ID}', process.env.CLIENT_ID);
    envFileContent = envFileContent.replace('${REDIRECT_URI}', process.env.REDIRECT_URI);

    fs.writeFileSync(filePath, envFileContent);
});