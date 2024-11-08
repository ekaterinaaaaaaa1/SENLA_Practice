CREATE TABLE inactivepassports (
    PASSP_SERIES smallint,
    PASSP_NUMBER integer,
    ACTIVE boolean,
    PRIMARY KEY (PASSP_SERIES, PASSP_NUMBER)
);

CREATE TEMP TABLE temp_inactivepassports (
    PASSP_SERIES char(4),
    PASSP_NUMBER char(6)
);

COPY temp_inactivepassports (PASSP_SERIES, PASSP_NUMBER) FROM '//passportsdata//Data.csv' DELIMITER ',' CSV HEADER;

INSERT INTO inactivepassports SELECT CAST(PASSP_SERIES AS smallint), CAST(PASSP_NUMBER AS integer), FALSE FROM temp_inactivepassports WHERE PASSP_SERIES ~ '^[0-9]{4}$' AND PASSP_NUMBER ~ '^[0-9]{6}$';

DROP TABLE temp_inactivepassports;