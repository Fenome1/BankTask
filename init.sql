--
-- PostgreSQL database dump
--

-- Dumped from database version 16.3 (Debian 16.3-1.pgdg120+1)
-- Dumped by pg_dump version 16.3

-- Started on 2024-07-09 14:54:03

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

--
-- TOC entry 3421 (class 1262 OID 16675)
-- Name: bank; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE bank WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE bank OWNER TO postgres;

\connect bank

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
-- TOC entry 215 (class 1259 OID 16682)
-- Name: accounts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.accounts (
    account_id integer NOT NULL,
    owner_id integer NOT NULL,
    number character varying(20) NOT NULL,
    name character varying(120),
    balance money DEFAULT 0 NOT NULL,
    currency_id integer NOT NULL
);


ALTER TABLE public.accounts OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16686)
-- Name: PersonalAccount_PersonalAccountId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."PersonalAccount_PersonalAccountId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."PersonalAccount_PersonalAccountId_seq" OWNER TO postgres;

--
-- TOC entry 3422 (class 0 OID 0)
-- Dependencies: 216
-- Name: PersonalAccount_PersonalAccountId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."PersonalAccount_PersonalAccountId_seq" OWNED BY public.accounts.account_id;


--
-- TOC entry 217 (class 1259 OID 16687)
-- Name: refresh_tokens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.refresh_tokens (
    refresh_token_id integer NOT NULL,
    user_id integer NOT NULL,
    token text NOT NULL,
    expiration_date timestamp without time zone NOT NULL
);


ALTER TABLE public.refresh_tokens OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16692)
-- Name: RefreshToken_RefreshTokenId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."RefreshToken_RefreshTokenId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."RefreshToken_RefreshTokenId_seq" OWNER TO postgres;

--
-- TOC entry 3423 (class 0 OID 0)
-- Dependencies: 218
-- Name: RefreshToken_RefreshTokenId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."RefreshToken_RefreshTokenId_seq" OWNED BY public.refresh_tokens.refresh_token_id;


--
-- TOC entry 219 (class 1259 OID 16693)
-- Name: transactions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transactions (
    transaction_id integer NOT NULL,
    amount money NOT NULL,
    "from_account_Id" integer NOT NULL,
    to_account_id integer NOT NULL,
    transfer_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    from_currency_id integer NOT NULL,
    to_currency_id integer NOT NULL
);


ALTER TABLE public.transactions OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16697)
-- Name: Transaction_TransactionId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Transaction_TransactionId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."Transaction_TransactionId_seq" OWNER TO postgres;

--
-- TOC entry 3424 (class 0 OID 0)
-- Dependencies: 220
-- Name: Transaction_TransactionId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Transaction_TransactionId_seq" OWNED BY public.transactions.transaction_id;


--
-- TOC entry 221 (class 1259 OID 16698)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    login character varying(50) NOT NULL,
    password character varying(255) NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 16701)
-- Name: User_UserId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."User_UserId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."User_UserId_seq" OWNER TO postgres;

--
-- TOC entry 3425 (class 0 OID 0)
-- Dependencies: 222
-- Name: User_UserId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."User_UserId_seq" OWNED BY public.users.user_id;


--
-- TOC entry 223 (class 1259 OID 16702)
-- Name: currencies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.currencies (
    currency_id integer NOT NULL,
    code character varying(3) NOT NULL,
    name character varying(50) NOT NULL
);


ALTER TABLE public.currencies OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 16705)
-- Name: currency_currency_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.currency_currency_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.currency_currency_id_seq OWNER TO postgres;

--
-- TOC entry 3426 (class 0 OID 0)
-- Dependencies: 224
-- Name: currency_currency_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.currency_currency_id_seq OWNED BY public.currencies.currency_id;


--
-- TOC entry 225 (class 1259 OID 16706)
-- Name: exchange_rates; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.exchange_rates (
    exchange_rate_id integer NOT NULL,
    currency_from integer NOT NULL,
    rate numeric(10,6) NOT NULL,
    created_date timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    currency_to integer NOT NULL
);


ALTER TABLE public.exchange_rates OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 16710)
-- Name: exchange_rates_exchange_rate_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.exchange_rates_exchange_rate_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.exchange_rates_exchange_rate_id_seq OWNER TO postgres;

--
-- TOC entry 3427 (class 0 OID 0)
-- Dependencies: 226
-- Name: exchange_rates_exchange_rate_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.exchange_rates_exchange_rate_id_seq OWNED BY public.exchange_rates.exchange_rate_id;


--
-- TOC entry 3228 (class 2604 OID 16711)
-- Name: accounts account_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts ALTER COLUMN account_id SET DEFAULT nextval('public."PersonalAccount_PersonalAccountId_seq"'::regclass);


--
-- TOC entry 3234 (class 2604 OID 16712)
-- Name: currencies currency_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.currencies ALTER COLUMN currency_id SET DEFAULT nextval('public.currency_currency_id_seq'::regclass);


--
-- TOC entry 3235 (class 2604 OID 16713)
-- Name: exchange_rates exchange_rate_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exchange_rates ALTER COLUMN exchange_rate_id SET DEFAULT nextval('public.exchange_rates_exchange_rate_id_seq'::regclass);


--
-- TOC entry 3230 (class 2604 OID 16714)
-- Name: refresh_tokens refresh_token_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refresh_tokens ALTER COLUMN refresh_token_id SET DEFAULT nextval('public."RefreshToken_RefreshTokenId_seq"'::regclass);


--
-- TOC entry 3231 (class 2604 OID 16715)
-- Name: transactions transaction_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions ALTER COLUMN transaction_id SET DEFAULT nextval('public."Transaction_TransactionId_seq"'::regclass);


--
-- TOC entry 3233 (class 2604 OID 16716)
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public."User_UserId_seq"'::regclass);


--
-- TOC entry 3404 (class 0 OID 16682)
-- Dependencies: 215
-- Data for Name: accounts; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3412 (class 0 OID 16702)
-- Dependencies: 223
-- Data for Name: currencies; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.currencies VALUES (1, 'RUB', 'Российский рубль');
INSERT INTO public.currencies VALUES (2, 'TGK', 'Энерджи коин');


--
-- TOC entry 3414 (class 0 OID 16706)
-- Dependencies: 225
-- Data for Name: exchange_rates; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.exchange_rates VALUES (1, 2, 5.000000, '2024-07-08 14:05:16.696101', 1);
INSERT INTO public.exchange_rates VALUES (2, 1, 0.200000, '2024-07-08 14:05:16.696101', 2);


--
-- TOC entry 3406 (class 0 OID 16687)
-- Dependencies: 217
-- Data for Name: refresh_tokens; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3408 (class 0 OID 16693)
-- Dependencies: 219
-- Data for Name: transactions; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3410 (class 0 OID 16698)
-- Dependencies: 221
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3428 (class 0 OID 0)
-- Dependencies: 216
-- Name: PersonalAccount_PersonalAccountId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."PersonalAccount_PersonalAccountId_seq"', 15, true);


--
-- TOC entry 3429 (class 0 OID 0)
-- Dependencies: 218
-- Name: RefreshToken_RefreshTokenId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."RefreshToken_RefreshTokenId_seq"', 53, true);


--
-- TOC entry 3430 (class 0 OID 0)
-- Dependencies: 220
-- Name: Transaction_TransactionId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Transaction_TransactionId_seq"', 22, true);


--
-- TOC entry 3431 (class 0 OID 0)
-- Dependencies: 222
-- Name: User_UserId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_UserId_seq"', 8, true);


--
-- TOC entry 3432 (class 0 OID 0)
-- Dependencies: 224
-- Name: currency_currency_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.currency_currency_id_seq', 3, true);


--
-- TOC entry 3433 (class 0 OID 0)
-- Dependencies: 226
-- Name: exchange_rates_exchange_rate_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.exchange_rates_exchange_rate_id_seq', 2, true);


--
-- TOC entry 3238 (class 2606 OID 16718)
-- Name: accounts account_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT account_pkey PRIMARY KEY (account_id);


--
-- TOC entry 3249 (class 2606 OID 16720)
-- Name: currencies currency_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.currencies
    ADD CONSTRAINT currency_pkey PRIMARY KEY (currency_id);


--
-- TOC entry 3251 (class 2606 OID 16722)
-- Name: exchange_rates exchange_rates_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exchange_rates
    ADD CONSTRAINT exchange_rates_pkey PRIMARY KEY (exchange_rate_id);


--
-- TOC entry 3241 (class 2606 OID 16724)
-- Name: refresh_tokens refresh_token_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refresh_tokens
    ADD CONSTRAINT refresh_token_pkey PRIMARY KEY (refresh_token_id);


--
-- TOC entry 3244 (class 2606 OID 16726)
-- Name: transactions transaction_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transaction_pkey PRIMARY KEY (transaction_id);


--
-- TOC entry 3246 (class 2606 OID 16728)
-- Name: users user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT user_pkey PRIMARY KEY (user_id);


--
-- TOC entry 3239 (class 1259 OID 16729)
-- Name: accounts_number_uq; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX accounts_number_uq ON public.accounts USING btree (number) WITH (deduplicate_items='true');


--
-- TOC entry 3247 (class 1259 OID 16730)
-- Name: currencies_code_uq; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX currencies_code_uq ON public.currencies USING btree (code) WITH (deduplicate_items='true');


--
-- TOC entry 3242 (class 1259 OID 16731)
-- Name: uq_refresh_tokens; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX uq_refresh_tokens ON public.refresh_tokens USING btree (user_id) WITH (deduplicate_items='true');


--
-- TOC entry 3434 (class 0 OID 0)
-- Dependencies: 3242
-- Name: INDEX uq_refresh_tokens; Type: COMMENT; Schema: public; Owner: postgres
--

COMMENT ON INDEX public.uq_refresh_tokens IS 'UQ UserId';


--
-- TOC entry 3252 (class 2606 OID 24700)
-- Name: accounts accounts_currency_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT accounts_currency_id_fkey FOREIGN KEY (currency_id) REFERENCES public.currencies(currency_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3253 (class 2606 OID 16762)
-- Name: accounts accounts_owner_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.accounts
    ADD CONSTRAINT accounts_owner_id_fkey FOREIGN KEY (owner_id) REFERENCES public.users(user_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3259 (class 2606 OID 24675)
-- Name: exchange_rates exchange_rates_currency_from_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exchange_rates
    ADD CONSTRAINT exchange_rates_currency_from_fkey FOREIGN KEY (currency_from) REFERENCES public.currencies(currency_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3260 (class 2606 OID 24680)
-- Name: exchange_rates exchange_rates_currency_to_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.exchange_rates
    ADD CONSTRAINT exchange_rates_currency_to_fkey FOREIGN KEY (currency_to) REFERENCES public.currencies(currency_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3254 (class 2606 OID 16757)
-- Name: refresh_tokens refresh_tokens_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.refresh_tokens
    ADD CONSTRAINT refresh_tokens_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3255 (class 2606 OID 16777)
-- Name: transactions transactions_from_account_Id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT "transactions_from_account_Id_fkey" FOREIGN KEY ("from_account_Id") REFERENCES public.accounts(account_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3256 (class 2606 OID 24690)
-- Name: transactions transactions_from_currency_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_from_currency_id_fkey FOREIGN KEY (from_currency_id) REFERENCES public.currencies(currency_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3257 (class 2606 OID 16782)
-- Name: transactions transactions_to_account_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_to_account_id_fkey FOREIGN KEY (to_account_id) REFERENCES public.accounts(account_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3258 (class 2606 OID 24695)
-- Name: transactions transactions_to_currency_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transactions
    ADD CONSTRAINT transactions_to_currency_id_fkey FOREIGN KEY (to_currency_id) REFERENCES public.currencies(currency_id) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


-- Completed on 2024-07-09 14:54:03

--
-- PostgreSQL database dump complete
--

