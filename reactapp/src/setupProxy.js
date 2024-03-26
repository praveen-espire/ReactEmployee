const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/employee",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7098',
        secure: false
    });

    app.use(appProxy);
};
