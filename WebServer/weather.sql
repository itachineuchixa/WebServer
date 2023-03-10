--
-- PostgreSQL database dump
--

-- Dumped from database version 15.1
-- Dumped by pg_dump version 15.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: cities; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.cities (
    id bigint NOT NULL,
    city character varying(89) NOT NULL,
    longitude double precision,
    latitude double precision
);


ALTER TABLE public.cities OWNER TO postgres;

--
-- Name: cities_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.cities_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.cities_id_seq OWNER TO postgres;

--
-- Name: cities_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.cities_id_seq OWNED BY public.cities.id;


--
-- Name: city_info; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.city_info (
    id bigint NOT NULL,
    max_weather double precision[] NOT NULL,
    min_weather double precision[] NOT NULL,
    period_weather double precision[] NOT NULL,
    current_weather double precision NOT NULL,
    city_id bigint NOT NULL
);


ALTER TABLE public.city_info OWNER TO postgres;

--
-- Name: city_info_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.city_info_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.city_info_id_seq OWNER TO postgres;

--
-- Name: city_info_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.city_info_id_seq OWNED BY public.city_info.id;


--
-- Name: user_info; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.user_info (
    id bigint NOT NULL,
    user_id bigint NOT NULL,
    city_id bigint NOT NULL
);


ALTER TABLE public.user_info OWNER TO postgres;

--
-- Name: user_info_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.user_info_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.user_info_id_seq OWNER TO postgres;

--
-- Name: user_info_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.user_info_id_seq OWNED BY public.user_info.id;


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id bigint NOT NULL,
    username character varying(50) NOT NULL,
    password character varying(50) NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- Name: cities id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cities ALTER COLUMN id SET DEFAULT nextval('public.cities_id_seq'::regclass);


--
-- Name: city_info id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.city_info ALTER COLUMN id SET DEFAULT nextval('public.city_info_id_seq'::regclass);


--
-- Name: user_info id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_info ALTER COLUMN id SET DEFAULT nextval('public.user_info_id_seq'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- Data for Name: cities; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.cities (id, city, longitude, latitude) FROM stdin;
13	╨Ю╤А╨╡╤Е╨╛╨▓╨╛-╨Ч╤Г╨╡╨▓╨╛	38.96178	55.80672
14	╨Ы╨╕╨║╨╕╨╜╨╛-╨Ф╤Г╨╗╨╡╨▓╨╛	38.9542	55.7083
\.


--
-- Data for Name: city_info; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.city_info (id, max_weather, min_weather, period_weather, current_weather, city_id) FROM stdin;
6	{-9.2,-11.5,-10.7,-7.5,-8.8,-9.8,-7.3,-8.7,-6,-3.5,0,0.7,1.8,0.7,-3.4,-5.6,-2.3,-4.2,-4.3,-2.7,-2.8,-2.7,1,1.7,1.8,0.1,-5.4,-2.1,-1.7,-1.3,0.5,0.7}	{-14.7,-15.9,-16,-14.6,-13,-14.9,-12.9,-12,-10.9,-6.5,-4.6,-0.7,0.8,-2.9,-7.1,-7.9,-8.3,-9.2,-9,-12,-11.8,-13.7,-2.2,0.4,0.4,-7.9,-9.2,-9.1,-3.9,-4.6,-4.1,-0.1}	{-12,-12.2,-12.4,-13.3,-13.3,-13.4,-13.5,-13.6,-13.6,-14.7,-13.7,-12.6,-11.5,-10.7,-10.2,-9.2,-10.4,-11.2,-11.7,-12,-12.2,-13.5,-13.5,-13.7,-13.6,-13.4,-13.4,-14.4,-14.2,-14.1,-13.8,-13.7,-13.7,-15.9,-15.3,-14.9,-14.3,-13.7,-13.2,-11.5,-12.1,-12.6,-13.2,-13.5,-13.6,-14.7,-14.5,-14.4,-14.6,-14.8,-14.8,-15.8,-15.5,-15.5,-15.5,-15.6,-15.7,-16,-15.2,-13.9,-12.7,-11.9,-11.6,-10.7,-11.8,-12.5,-12.9,-13.2,-13.6,-13.6,-13.9,-14.2,-14.4,-14.5,-14.6,-14.3,-14.3,-14.1,-13.6,-12.6,-11.8,-11,-9.7,-8.6,-7.9,-7.6,-7.5,-7.6,-7.8,-7.9,-7.9,-8,-8.2,-8,-8.2,-8.8,-9.6,-10.5,-11.4,-11.6,-12.1,-12.3,-12.5,-12.6,-12.7,-13,-12.3,-11.3,-10.4,-9.7,-9.5,-8.8,-10,-10.7,-11.2,-11.5,-11.8,-12.3,-12.6,-12.9,-13.2,-13.5,-13.7,-14.3,-14.4,-14.5,-14.6,-14.7,-14.8,-14.9,-14.2,-12.9,-11.8,-11,-10.7,-9.8,-10.9,-11.7,-12.2,-12.6,-13,-12.4,-12.4,-12.2,-12,-12,-12,-12.1,-12.2,-12.6,-12.8,-12.9,-12.9,-11.9,-11.3,-10.2,-9.2,-8.6,-8.5,-7.3,-8.3,-8.8,-9.2,-9.4,-9.6,-8.7,-8.9,-9,-9.3,-9.8,-10.1,-10.2,-10.5,-10.9,-11.2,-11.5,-11.8,-12,-11.6,-10.9,-10.1,-9.6,-9.4,-8.7,-9.5,-10,-10.3,-10.5,-10.6,-10.2,-10.4,-10.6,-10.7,-10.8,-10.7,-10.9,-10.8,-10.8,-10.6,-10.6,-10.6,-10,-9.9,-9.7,-9.3,-9,-8.7,-8.3,-8.4,-8.5,-8.5,-8.2,-7.9,-6.4,-6.3,-6,-5.9,-5.8,-5.7,-5.6,-5.6,-6,-6.3,-6.5,-6.2,-4.9,-4.9,-4.4,-4.1,-3.7,-3.5,-4,-4.5,-4.9,-5.2,-5.5,-5.7,-5.2,-5.2,-4.9,-4.6,-4.4,-4.1,-3.5,-3.4,-3.3,-3.3,-3.2,-3.2,-2.4,-2.1,-1.4,-0.9,-0.3,-0.3,-0.4,-0.6,-0.7,-0.7,-0.7,-0.7,-0.2,-0.1,0,0.1,0.1,0.2,0.2,0.1,-0.1,-0.2,-0.4,-0.7,-0.1,0.1,-0,-0,-0,0,-0,-0.1,-0.2,-0.2,-0,0.1,0.4,0.5,0.7,0.9,1,1.2,1.6,1.5,1.3,1.1,1,1,1.8,1.7,1.6,1.5,1.5,1.4,1.6,1.4,1.1,0.9,0.8,0.8,1.3,1.1,1,0.6,0.6,0.6,0.7,0.6,0.5,0.4,0.4,0.3,0.5,0.5,0.5,0.5,0.4,0.3,0.4,0.1,-0.4,-0.8,-1.2,-1.6,-1.6,-2.2,-2.9,-3.4,-3.9,-4.9,-5.8,-6.5,-6.9,-7.1,-7.1,-6.9,-5.7,-5.5,-4.9,-4.4,-4,-4,-4.5,-5,-5.5,-5.8,-6.3,-6.9,-5,-5.4,-5.5,-5.6,-6.1,-7.1,-6.2,-6.4,-7,-7.6,-7.8,-7.9,-7.6,-7.7,-6.9,-5.9,-5.7,-5.6,-6.4,-7,-7.2,-7.3,-7.3,-6.9,-6.4,-6,-5.8,-5.7,-5.5,-5.4,-4.7,-4.6,-4.5,-4.3,-4.1,-3.7,-2.5,-2.3,-2.3,-2.8,-3.4,-3.9,-3.7,-4.8,-5.5,-6.2,-7.3,-8.3,-6.7,-7.5,-8,-8.9,-9.1,-9.2,-7.4,-7,-6.8,-6.6,-6.6,-6.3,-5.7,-5.3,-4.9,-4.5,-4.3,-4.2,-4.3,-4.6,-4.7,-4.8,-4.8,-4.8,-4.4,-4.4,-4.4,-4.5,-4.5,-4.6,-4.3,-4.4,-4.6,-4.9,-5.2,-5.5,-5.4,-5.6,-5.7,-5.7,-5.7,-5.9,-5.6,-5.9,-6,-6.3,-6.6,-7,-7.3,-8.1,-9,-9.7,-10.2,-10.6,-10.7,-11.5,-12,-11.9,-11.5,-11,-9.9,-9.1,-8.3,-7.3,-6.4,-5.7,-5.3,-5,-4.7,-4.6,-4.1,-3.9,-3,-2.7,-2.7,-2.8,-3.2,-3.3,-2.9,-3,-3.5,-3.9,-4,-4.5,-5.6,-6.6,-7.2,-7.3,-7.3,-7.3,-6.7,-7.8,-8.9,-9.9,-10.5,-11.3,-10.9,-11.4,-11.8,-12.3,-12.6,-12.9,-13.4,-13.6,-13.7,-13.5,-13.4,-13.2,-11.5,-10.9,-9.8,-8.4,-7.1,-6.1,-6,-5.7,-5.7,-5.3,-5,-4.6,-3.4,-3.1,-2.7,-2.2,-1.8,-1.5,-0.8,-0.3,0.1,0.5,0.7,0.6,0.9,0.9,0.8,0.8,0.8,0.7,1,0.9,0.8,0.7,0.6,0.6,0.8,0.7,0.8,0.7,0.6,0.6,0.5,0.4,0.5,0.5,0.6,0.6,0.7,0.7,0.8,0.8,0.9,1,1,1,1.1,1,1,1,1.7,1.7,1.7,1.8,1.6,1.5,1.7,1.5,1.3,1.2,1.1,1,1.4,1.3,1.4,1.5,1.6,1.6,1.5,1.2,0.9,0.5,0.4,0.4,0.9,0.8,0.7,0.1,-0.7,-1,-0.4,-0.2,-0.5,-1.1,-2.7,-4,-3.7,-4.2,-4.1,-3.6,-3.1,-3.1,-3.3,-4,-5.1,-6,-6.6,-7,-6.1,-7.2,-7.9,-8.5,-8.9,-9.2,-7.5,-7.7,-7.9,-8.1,-8.1,-8.3,-6.4,-6.7,-6.5,-6.2,-5.7,-5.4,-6.2,-6.7,-7.5,-8,-8.4,-8.6,-7.2,-7.6,-7.9,-7.9,-7.9,-8,-7,-7.4,-7.8,-8.2,-8.7,-9.1,-7.9,-7.9,-7.4,-6.5,-5.8,-5.2,-5.2,-4.8,-4.4,-4.1,-3.8,-3.4,-2.6,-2.3,-2.1,-1.9,-1.8,-1.7,-1.9,-2,-2.1,-2.2,-2.4,-2.5,-2.6,-2.7,-2.7,-2.5,-2.3,-2.1,-2.8,-2.9,-3.3,-3.6,-3.8,-3.9,-3.2,-3.1,-2.9,-2.9,-3,-3.1,-2.8,-2.5,-2.2,-2,-2.1,-2.3,-1.3,-1.9,-1.9,-2.1,-2,-2.2,-3,-3.5,-3.9,-4.3,-4.6,-4.6,-3.8,-4.2,-4.2,-4.1,-3.9,-3.9,-3.3,-3.2,-2.9,-2.6,-2.4,-2.1,-1.9,-1.7,-1.3,-0.8,-0.4,-0.1,0,0,0,0.1,0.3,0.4,0.5,0.5,0.5,0.5,0.5,0.5,0.3,0.2,0.2,0.1,0.1,0.1,0.1,0.2,0.5,0.7,0.7,0.7,0.7,0.6,0.6,0.5,0.4,-0,-0.1,0.1,0.4}	0.7	13
7	{-9.1,-11.1,-10.2,-7.5,-8.9,-9.8,-7.3,-8.9,-6.1,-3.7,0.1,0.7,1.7,0.6,-3.7,-5.8,-2.3,-4,-4.2,-2.8,-3.1,-2.7,0.8,1.7,1.7,0.2,-5.5,-1.8,-1.6,-1.7,0.4,0.6}	{-14.5,-15.6,-15.4,-14.2,-13,-14.8,-13,-12.1,-11.1,-6.2,-4.4,-0.4,0.7,-3.2,-7,-8.6,-9.1,-8.8,-8.8,-12.3,-11.9,-13.4,-2.3,0.3,0.3,-7.7,-9.2,-9.2,-4,-4.4,-3.6,-0.2}	{-11.8,-12,-12.2,-13,-13,-13.1,-13.2,-13.3,-13.3,-14.5,-13.5,-12.5,-11.5,-10.6,-10.1,-9.1,-10.1,-10.9,-11.4,-11.6,-11.8,-13.2,-13.2,-13.3,-13.1,-13,-13,-14,-13.7,-13.7,-13.7,-13.6,-13.6,-15.6,-14.9,-14.5,-14,-13.4,-12.9,-11.1,-11.7,-12.3,-12.7,-13,-13,-14.3,-14,-14,-14.3,-14.4,-14.2,-15,-14.7,-14.5,-14.6,-14.9,-15.1,-15.4,-14.7,-13.5,-12.3,-11.4,-11,-10.2,-11.4,-12.1,-12.6,-13,-13.3,-13.3,-13.6,-13.9,-14.1,-14.2,-14.2,-14,-14,-13.8,-13.4,-12.4,-11.5,-10.6,-9.5,-8.5,-7.9,-7.6,-7.5,-7.6,-7.9,-8,-8,-8.2,-8.3,-8.2,-8.4,-9.1,-9.9,-10.9,-11.7,-11.8,-12.2,-12.3,-12.5,-12.6,-12.7,-13,-12.3,-11.3,-10.4,-9.7,-9.5,-8.9,-10,-10.8,-11.3,-11.6,-11.9,-12.4,-12.7,-13,-13.2,-13.5,-13.7,-14.3,-14.4,-14.5,-14.6,-14.6,-14.7,-14.8,-14.2,-12.9,-11.8,-11,-10.7,-9.8,-10.9,-11.7,-12.2,-12.6,-12.9,-12.4,-12.3,-12.2,-12.2,-12.1,-12.1,-12.2,-12.2,-12.5,-12.8,-13,-13,-11.9,-11.3,-10.2,-9.3,-8.7,-8.6,-7.3,-8.4,-8.9,-9.2,-9.5,-9.8,-8.9,-9.1,-9.2,-9.7,-10.1,-10.4,-10.4,-10.7,-11.1,-11.4,-11.7,-12,-12.1,-11.8,-11.1,-10.3,-9.8,-9.6,-8.9,-9.7,-10.2,-10.5,-10.7,-10.8,-10.4,-10.6,-10.7,-10.8,-11,-10.9,-11.1,-11,-10.9,-10.9,-10.9,-10.8,-10.2,-10.1,-9.8,-9.4,-9,-8.8,-8.4,-8.5,-8.6,-8.6,-8.2,-7.9,-6.5,-6.4,-6.1,-6,-5.9,-5.8,-5.7,-5.8,-6.1,-6.2,-6.2,-6,-5.1,-5,-4.7,-4.1,-3.8,-3.7,-4.1,-4.6,-5,-5.4,-5.6,-5.8,-5,-4.9,-4.6,-4.4,-4.2,-3.9,-3.3,-3.2,-3.1,-3,-3,-3,-2.1,-1.9,-1.3,-0.8,-0.4,-0.3,-0.4,-0.6,-0.7,-0.8,-0.7,-0.7,-0.2,-0.1,0.1,0.1,0,0.2,0.1,0.1,-0.1,-0.2,-0.3,-0.4,-0,-0,-0,-0,-0,-0,-0,0,-0,0,0.1,0.2,0.5,0.6,0.7,0.9,1,1.1,1.5,1.3,1.1,1,0.9,0.8,1.7,1.5,1.4,1.3,1.3,1.3,1.4,1.2,0.9,0.8,0.7,0.7,1.1,0.9,1,0.4,0.5,0.5,0.6,0.5,0.3,0.3,0.3,0.2,0.4,0.4,0.4,0.4,0.3,0.2,0.3,-0.1,-0.6,-1,-1.4,-1.8,-1.9,-2.6,-3.2,-3.7,-4.2,-5.2,-6,-6.5,-6.9,-6.9,-6.9,-6.9,-5.7,-5.6,-4.9,-4.3,-4,-4.1,-4.6,-5,-5.6,-5.9,-6.6,-7,-5.5,-5.6,-5.7,-5.9,-6.4,-7.1,-6.5,-7,-7.6,-8.3,-8.4,-8.6,-8.2,-8.2,-7.2,-6.1,-5.8,-6.1,-6.6,-7.1,-7.3,-7.5,-7.3,-7,-6.4,-6,-5.9,-5.8,-5.6,-5.5,-4.8,-4.7,-4.5,-4.3,-4.1,-3.7,-2.6,-2.3,-2.3,-2.7,-3.3,-3.9,-3.8,-5.2,-6.3,-7.1,-8.1,-9.1,-7.3,-7.7,-8.5,-8.8,-8.7,-8.6,-7.1,-6.8,-6.7,-6.6,-6.4,-6.1,-5.6,-5.2,-4.8,-4.4,-4.1,-4,-4.2,-4.3,-4.5,-4.6,-4.6,-4.6,-4.2,-4.2,-4.3,-4.3,-4.4,-4.5,-4.2,-4.3,-4.5,-4.8,-5.1,-5.4,-5.5,-5.6,-5.7,-5.7,-5.8,-6,-5.7,-6,-6.1,-6.4,-6.6,-7,-7.2,-8,-8.8,-9.6,-10.2,-10.6,-10.8,-11.6,-12.2,-12.3,-11.8,-11.3,-10.3,-9.5,-8.5,-7.5,-6.6,-5.9,-5.5,-5.1,-5,-4.8,-4.3,-4,-3.2,-2.9,-2.8,-3.1,-3.4,-3.4,-3.1,-3.2,-3.7,-4.1,-4.2,-4.9,-6.1,-7.1,-7.6,-7.6,-7.6,-7.6,-6.8,-7.9,-8.9,-9.8,-10.5,-11.2,-10.7,-11.4,-11.9,-12.3,-12.7,-13,-13,-13.2,-13.4,-13.1,-12.7,-12.4,-10.6,-10,-8.9,-7.5,-6.4,-5.6,-5.7,-5.6,-5.8,-5.4,-5,-4.6,-3.4,-3.1,-2.7,-2.3,-1.9,-1.5,-0.8,-0.4,0,0.4,0.5,0.4,0.6,0.7,0.7,0.6,0.6,0.6,0.8,0.7,0.7,0.5,0.4,0.4,0.7,0.6,0.6,0.6,0.5,0.3,0.4,0.4,0.4,0.4,0.5,0.4,0.6,0.6,0.7,0.7,0.8,0.8,0.9,1,0.9,0.9,0.9,0.9,1.6,1.6,1.7,1.6,1.5,1.4,1.7,1.5,1.3,1.1,1,0.9,1.1,1.2,1.3,1.4,1.6,1.6,1.6,1.4,1,0.6,0.3,0.3,0.9,0.8,0.7,0.2,-0.5,-1.2,-0.8,-0.4,-0.6,-1.3,-2.3,-3.9,-3.8,-4.3,-4.2,-3.8,-3.3,-3.2,-3.6,-4.3,-5.3,-6.3,-7,-7.4,-6.2,-7.1,-7.7,-8.5,-8.8,-9.2,-8,-8.2,-8.3,-8.3,-8.2,-8.4,-6.5,-6.8,-6.7,-6.4,-5.8,-5.5,-6.2,-6.7,-7.4,-8,-8.2,-8.4,-7,-7.5,-7.6,-7.6,-7.7,-7.9,-7.1,-7.4,-7.8,-8.4,-8.9,-9.2,-7.9,-7.9,-7.3,-6.4,-5.6,-5,-5,-4.6,-4.4,-4.1,-3.7,-3.3,-2.5,-2.1,-1.8,-1.7,-1.6,-1.6,-1.8,-1.9,-2.1,-2.3,-2.4,-2.5,-2.6,-2.6,-2.6,-2.5,-2.4,-2.2,-2.8,-2.9,-3.3,-3.7,-4,-4,-3.1,-3.1,-3,-3,-2.9,-2.8,-2.3,-2.1,-2.2,-2.1,-2.2,-2.6,-1.7,-2.2,-2.2,-2.3,-2.4,-2.6,-3.2,-3.4,-3.6,-3.9,-4.3,-4.4,-3.8,-4,-3.8,-3.6,-3.5,-3.5,-2.7,-2.6,-2.4,-2.2,-2,-1.9,-1.6,-1.5,-1.2,-0.8,-0.4,-0.1,-0,-0,-0,0.1,0.2,0.3,0.4,0.4,0.4,0.4,0.4,0.4,0.1,0.1,0.1,0,-0,-0.1,0,0.1,0.3,0.5,0.6,0.6,0.5,0.5,0.5,0.4,0.3,-0.2,-0.2,0.1,0.3}	0.5	14
\.


--
-- Data for Name: user_info; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.user_info (id, user_id, city_id) FROM stdin;
18	1	13
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (id, username, password) FROM stdin;
1	1234	1234
5	12345	1234
6	Neuchixa	1234
7	╨г╤З╨╕╤Е╨░	╨г╤З╨╕╨╖╨╛╨▓╨╕╤З
8	╨г╤З╨╕╤Е╨░1	1234
\.


--
-- Name: cities_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.cities_id_seq', 14, true);


--
-- Name: city_info_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.city_info_id_seq', 7, true);


--
-- Name: user_info_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_info_id_seq', 19, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 8, true);


--
-- Name: cities cities_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.cities
    ADD CONSTRAINT cities_pkey PRIMARY KEY (id);


--
-- Name: city_info city_info_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.city_info
    ADD CONSTRAINT city_info_pkey PRIMARY KEY (id);


--
-- Name: user_info user_info_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_info
    ADD CONSTRAINT user_info_pkey PRIMARY KEY (id);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- Name: city_info city_info_city_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.city_info
    ADD CONSTRAINT city_info_city_id_fkey FOREIGN KEY (city_id) REFERENCES public.cities(id);


--
-- Name: user_info user_info_city_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_info
    ADD CONSTRAINT user_info_city_id_fkey FOREIGN KEY (city_id) REFERENCES public.cities(id);


--
-- Name: user_info user_info_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_info
    ADD CONSTRAINT user_info_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);


--
-- PostgreSQL database dump complete
--

