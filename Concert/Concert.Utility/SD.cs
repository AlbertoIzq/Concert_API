﻿namespace Concert.Utility
{
    public class SD
    {
        public const string READER_ROLE_ID = "9bcd561e-6a99-4042-ae67-8e519dd0be08";
        public const string WRITER_ROLE_ID = "bec9037a-d166-441e-9684-a73b9346ccdc";
        public const string READER_ROLE_NAME = "Reader";
        public const string WRITER_ROLE_NAME = "Writer";

        public const int SONG_DEFAULT_PAGE_SIZE = 1000;

        public const int JWT_TOKEN_EXPIRATION_MINUTES = 15;

        public static readonly string[] IMAGE_ALLOWED_EXTENSIONS = [".jpg", ".jpeg", ".png"];
        public const long IMAGE_MAX_LENGTH_BYTES = 10485760; // 10 MB
        public const string IMAGES_FOLDER_NAME = "Images";

        public const string LOGS_FILE_FULL_PATH = "Logs/Concert_logs_.txt";
    }
}