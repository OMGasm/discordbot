CREATE TABLE IF NOT EXISTS logSeverity
(
    id INTEGER PRIMARY KEY,
    severity TEXT
);

CREATE TABLE IF NOT EXISTS logs
(
    id INTEGER PRIMARY KEY,
    severity INTEGER,
    source TEXT,
    message TEXT,
    time INTEGER,
    FOREIGN KEY(severity) REFERENCES logSeverity(id)
);

CREATE TABLE IF NOT EXISTS users
(
    id INTEGER PRIMARY KEY,
    name TEXT,
    discriminator INTEGER,
    joinedAt INTEGER,
    mention TEXT
);

CREATE TABLE IF NOT EXISTS regions
(
    id INTEGER PRIMARY KEY,
    regionID TEXT,
    name TEXT,
    hostname TEXT,
    port INTEGER,
    vip INTEGER
);

CREATE TABLE IF NOT EXISTS servers
(
    id INTEGER PRIMARY KEY,
    name TEXT,
    owner INTEGER,
    region INTEGER,
    FOREIGN KEY(owner) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS channels
(
    id INTEGER PRIMARY KEY,
    name,
    server INTEGER,
    private INTEGER,
    mention TEXT,
    topic TEXT,
    type TEXT,
    FOREIGN KEY(server) REFERENCES servers(id),
    FOREIGN KEY(private) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS messages
(
    id INTEGER PRIMARY KEY,
    channelID INTEGER,
    userID INTEGER,
    message TEXT,
    time INTEGER,
    FOREIGN KEY(channelID) REFERENCES channels(id),
    FOREIGN KEY(userID) REFERENCES users(id)
);

CREATE TABLE IF NOT EXISTS attachments
(
    id INTEGER PRIMARY KEY,
    attachmentID TEXT,
    messageID INTEGER,
    filename TEXT,
    size INTEGER,
    proxyURL TEXT,
    url TEXT,
    FOREIGN KEY(messageID) REFERENCES messages(id)
);

CREATE TABLE IF NOT EXISTS attachmentImages
(
    id INTEGER PRIMARY KEY,
    attachmentID INTEGER,
    imageWidth INTEGER,
    imageHeight INTEGER,
    FOREIGN KEY(attachmentID) REFERENCES attachments(id)
);

CREATE TABLE IF NOT EXISTS embeds
(
    id INTEGER PRIMARY KEY,
    messageID INTEGER,
    url TEXT,
    title TEXT,
    description TEXT,
    thumbnailProxyURL TEXT,
    thumbnailURL TEXT,
    thumbnailWidth INTEGER,
    thumbnailHeight INTEGER,
    providerName TEXT,
    providerURL TEXT,
    FOREIGN KEY(messageID) REFERENCES messages(id)
);

CREATE TABLE IF NOT EXISTS embedVideos
(
    id INTEGER PRIMARY KEY,
    embedID INTEGER,
    url TEXT,
    proxyURL TEXT,
    width INTEGER,
    height INTEGER,
    FOREIGN KEY(embedID) REFERENCES embeds(id)
);