-- MySQL Database Export
-- Generated from SQLite database
-- Database: simple ecommerce system

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


-- Table: auth_group
DROP TABLE IF EXISTS `auth_group`;
CREATE TABLE `auth_group` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `name` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Table: auth_group_permissions
DROP TABLE IF EXISTS `auth_group_permissions`;
CREATE TABLE `auth_group_permissions` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `group_id` INT NOT NULL,
  `permission_id` INT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Table: auth_permission
DROP TABLE IF EXISTS `auth_permission`;
CREATE TABLE `auth_permission` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `content_type_id` INT NOT NULL,
  `codename` TEXT NOT NULL,
  `name` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: auth_permission
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (1, 1, 'add_logentry', 'Can add log entry');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (2, 1, 'change_logentry', 'Can change log entry');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (3, 1, 'delete_logentry', 'Can delete log entry');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (4, 1, 'view_logentry', 'Can view log entry');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (5, 3, 'add_permission', 'Can add permission');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (6, 3, 'change_permission', 'Can change permission');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (7, 3, 'delete_permission', 'Can delete permission');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (8, 3, 'view_permission', 'Can view permission');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (9, 2, 'add_group', 'Can add group');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (10, 2, 'change_group', 'Can change group');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (11, 2, 'delete_group', 'Can delete group');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (12, 2, 'view_group', 'Can view group');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (13, 4, 'add_user', 'Can add user');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (14, 4, 'change_user', 'Can change user');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (15, 4, 'delete_user', 'Can delete user');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (16, 4, 'view_user', 'Can view user');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (17, 5, 'add_contenttype', 'Can add content type');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (18, 5, 'change_contenttype', 'Can change content type');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (19, 5, 'delete_contenttype', 'Can delete content type');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (20, 5, 'view_contenttype', 'Can view content type');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (21, 6, 'add_session', 'Can add session');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (22, 6, 'change_session', 'Can change session');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (23, 6, 'delete_session', 'Can delete session');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (24, 6, 'view_session', 'Can view session');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (25, 7, 'add_category', 'Can add category');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (26, 7, 'change_category', 'Can change category');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (27, 7, 'delete_category', 'Can delete category');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (28, 7, 'view_category', 'Can view category');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (29, 8, 'add_product', 'Can add product');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (30, 8, 'change_product', 'Can change product');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (31, 8, 'delete_product', 'Can delete product');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (32, 8, 'view_product', 'Can view product');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (33, 9, 'add_productreview', 'Can add product review');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (34, 9, 'change_productreview', 'Can change product review');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (35, 9, 'delete_productreview', 'Can delete product review');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (36, 9, 'view_productreview', 'Can view product review');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (37, 10, 'add_userprofile', 'Can add user profile');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (38, 10, 'change_userprofile', 'Can change user profile');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (39, 10, 'delete_userprofile', 'Can delete user profile');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (40, 10, 'view_userprofile', 'Can view user profile');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (41, 11, 'add_cart', 'Can add cart');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (42, 11, 'change_cart', 'Can change cart');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (43, 11, 'delete_cart', 'Can delete cart');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (44, 11, 'view_cart', 'Can view cart');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (45, 12, 'add_cartitem', 'Can add cart item');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (46, 12, 'change_cartitem', 'Can change cart item');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (47, 12, 'delete_cartitem', 'Can delete cart item');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (48, 12, 'view_cartitem', 'Can view cart item');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (49, 13, 'add_order', 'Can add order');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (50, 13, 'change_order', 'Can change order');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (51, 13, 'delete_order', 'Can delete order');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (52, 13, 'view_order', 'Can view order');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (53, 14, 'add_orderitem', 'Can add order item');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (54, 14, 'change_orderitem', 'Can change order item');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (55, 14, 'delete_orderitem', 'Can delete order item');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (56, 14, 'view_orderitem', 'Can view order item');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (57, 15, 'add_contactmessage', 'Can add contact message');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (58, 15, 'change_contactmessage', 'Can change contact message');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (59, 15, 'delete_contactmessage', 'Can delete contact message');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (60, 15, 'view_contactmessage', 'Can view contact message');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (61, 16, 'add_userreport', 'Can add user report');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (62, 16, 'change_userreport', 'Can change user report');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (63, 16, 'delete_userreport', 'Can delete user report');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (64, 16, 'view_userreport', 'Can view user report');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (65, 17, 'add_announcement', 'Can add announcement');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (66, 17, 'change_announcement', 'Can change announcement');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (67, 17, 'delete_announcement', 'Can delete announcement');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (68, 17, 'view_announcement', 'Can view announcement');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (69, 18, 'add_complaint', 'Can add complaint');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (70, 18, 'change_complaint', 'Can change complaint');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (71, 18, 'delete_complaint', 'Can delete complaint');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (72, 18, 'view_complaint', 'Can view complaint');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (73, 19, 'add_notification', 'Can add notification');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (74, 19, 'change_notification', 'Can change notification');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (75, 19, 'delete_notification', 'Can delete notification');
INSERT INTO `auth_permission` (`id`, `content_type_id`, `codename`, `name`) VALUES (76, 19, 'view_notification', 'Can view notification');


-- Table: auth_user
DROP TABLE IF EXISTS `auth_user`;
CREATE TABLE `auth_user` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `password` TEXT NOT NULL,
  `last_login` DATETIME,
  `is_superuser` TINYINT(1) NOT NULL,
  `username` TEXT NOT NULL,
  `last_name` TEXT NOT NULL,
  `email` TEXT NOT NULL,
  `is_staff` TINYINT(1) NOT NULL,
  `is_active` TINYINT(1) NOT NULL,
  `date_joined` DATETIME NOT NULL,
  `first_name` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: auth_user
INSERT INTO `auth_user` (`id`, `password`, `last_login`, `is_superuser`, `username`, `last_name`, `email`, `is_staff`, `is_active`, `date_joined`, `first_name`) VALUES (1, 'pbkdf2_sha256$1200000$eIxfGNq1s7jL6LKfHdJM2c$zXF9UqE5nI6FxoZjc0PrgLCTY1upXe/cDkiAiAv6jHk=', '2026-06-07 09:01:27.267012', 1, 'admin', '', 'admin@example.com', 1, 1, '2026-05-29 09:47:12.299960', '');
INSERT INTO `auth_user` (`id`, `password`, `last_login`, `is_superuser`, `username`, `last_name`, `email`, `is_staff`, `is_active`, `date_joined`, `first_name`) VALUES (2, 'pbkdf2_sha256$1200000$xI19RjKost5o65gQQhGEdM$XcJJcTQpgSSGsfrmDQLxEYZvGkN4ijLLOr/sFRkb2I4=', '2026-06-02 07:27:48.175752', 0, 'testuser', 'User', 'test@example.com', 0, 1, '2026-05-29 09:58:58', 'Test');
INSERT INTO `auth_user` (`id`, `password`, `last_login`, `is_superuser`, `username`, `last_name`, `email`, `is_staff`, `is_active`, `date_joined`, `first_name`) VALUES (3, 'pbkdf2_sha256$1200000$Blp5qEzsGTPlCfVJEcGq5w$GebJQ2+rxXhJmisRhMBtElcTyfnDjHvEZko/nzP1vXE=', NULL, 0, 'john', 'Doe', 'john@example.com', 0, 1, '2026-05-29 09:59:00.557847', 'John');
INSERT INTO `auth_user` (`id`, `password`, `last_login`, `is_superuser`, `username`, `last_name`, `email`, `is_staff`, `is_active`, `date_joined`, `first_name`) VALUES (4, 'pbkdf2_sha256$1200000$YlsgVEGZx7RbwXuOMGwCtN$4r6fXrqagMn9W/6jTuy4N86wvZncE33cJEHylXs7Nto=', NULL, 0, 'jane', 'Smith', 'jane@example.com', 0, 1, '2026-05-29 09:59:02.191577', 'Jane');
INSERT INTO `auth_user` (`id`, `password`, `last_login`, `is_superuser`, `username`, `last_name`, `email`, `is_staff`, `is_active`, `date_joined`, `first_name`) VALUES (5, 'pbkdf2_sha256$1200000$HvvcODhlzYsHI8MuLfDLwp$PhiD7oM/WrI4/fBs69NX/kqA1ty8ap8F1KrUHppJ3GM=', NULL, 0, 'customer', 'Demo', 'customer@example.com', 0, 1, '2026-05-29 09:59:03.652466', 'Customer');
INSERT INTO `auth_user` (`id`, `password`, `last_login`, `is_superuser`, `username`, `last_name`, `email`, `is_staff`, `is_active`, `date_joined`, `first_name`) VALUES (6, 'pbkdf2_sha256$1200000$2DqIb0INZi3qgTNvOcUtaX$Sc4KfAJqsOC+2r4EP0GIyv93XvEMvIUc13yAZd3EB/o=', '2026-05-29 14:01:36.226698', 0, 'aku1603563', '', 'eliasshumuye8@gmail.com', 0, 0, '2026-05-29 12:54:53.231705', '');
INSERT INTO `auth_user` (`id`, `password`, `last_login`, `is_superuser`, `username`, `last_name`, `email`, `is_staff`, `is_active`, `date_joined`, `first_name`) VALUES (7, 'pbkdf2_sha256$1200000$cmuo2GwTGGSYFcYghfloTg$rwAkoHF2yaz48ujYRrybOyzl+w9fnhdjgZhsrI/tKDc=', '2026-06-07 08:57:33.550541', 0, 'ella12', '', 'eliasshumuyeela@gmail.com', 0, 1, '2026-06-07 08:57:20.071723', '');


-- Table: auth_user_groups
DROP TABLE IF EXISTS `auth_user_groups`;
CREATE TABLE `auth_user_groups` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `user_id` INT NOT NULL,
  `group_id` INT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Table: auth_user_user_permissions
DROP TABLE IF EXISTS `auth_user_user_permissions`;
CREATE TABLE `auth_user_user_permissions` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `user_id` INT NOT NULL,
  `permission_id` INT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Table: cart_cart
DROP TABLE IF EXISTS `cart_cart`;
CREATE TABLE `cart_cart` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  `user_id` INT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: cart_cart
INSERT INTO `cart_cart` (`id`, `created_at`, `updated_at`, `user_id`) VALUES (1, '2026-05-29 10:51:15.446383', '2026-05-29 10:51:15.446423', 1);
INSERT INTO `cart_cart` (`id`, `created_at`, `updated_at`, `user_id`) VALUES (2, '2026-05-29 13:02:14.508063', '2026-05-29 13:02:14.508109', 6);
INSERT INTO `cart_cart` (`id`, `created_at`, `updated_at`, `user_id`) VALUES (3, '2026-05-31 06:42:30.277842', '2026-05-31 06:42:30.277870', 2);
INSERT INTO `cart_cart` (`id`, `created_at`, `updated_at`, `user_id`) VALUES (4, '2026-06-07 08:58:08.701426', '2026-06-07 08:58:08.701461', 7);


-- Table: cart_cartitem
DROP TABLE IF EXISTS `cart_cartitem`;
CREATE TABLE `cart_cartitem` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `quantity` INT NOT NULL,
  `added_at` DATETIME NOT NULL,
  `cart_id` BIGINT NOT NULL,
  `product_id` BIGINT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: cart_cartitem
INSERT INTO `cart_cartitem` (`id`, `quantity`, `added_at`, `cart_id`, `product_id`) VALUES (4, 6, '2026-05-29 16:36:57.091810', 1, 12);
INSERT INTO `cart_cartitem` (`id`, `quantity`, `added_at`, `cart_id`, `product_id`) VALUES (5, 1, '2026-05-29 17:52:54.105209', 1, 15);


-- Table: orders_order
DROP TABLE IF EXISTS `orders_order`;
CREATE TABLE `orders_order` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `order_id` TEXT NOT NULL,
  `total_price` DECIMAL(10,2) NOT NULL,
  `full_name` TEXT NOT NULL,
  `email` TEXT NOT NULL,
  `phone` TEXT NOT NULL,
  `shipping_address` TEXT NOT NULL,
  `city` TEXT NOT NULL,
  `state` TEXT NOT NULL,
  `zipcode` TEXT NOT NULL,
  `payment_method` TEXT NOT NULL,
  `payment_status` TINYINT(1) NOT NULL,
  `order_status` TEXT NOT NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  `user_id` INT NOT NULL,
  `payment_reference` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: orders_order
INSERT INTO `orders_order` (`id`, `order_id`, `total_price`, `full_name`, `email`, `phone`, `shipping_address`, `city`, `state`, `zipcode`, `payment_method`, `payment_status`, `order_status`, `created_at`, `updated_at`, `user_id`, `payment_reference`) VALUES (1, 'ORD-740A0540', 599.94, 'Elias Shumuye', 'eliasshumuye8@gmail.com', '0956037886', 'shjire', 'addis ababa', 'shire', '0000', 'cod', 1, 'delivered', '2026-05-29 13:03:01.807709', '2026-05-31 19:58:06.769736', 6, '');
INSERT INTO `orders_order` (`id`, `order_id`, `total_price`, `full_name`, `email`, `phone`, `shipping_address`, `city`, `state`, `zipcode`, `payment_method`, `payment_status`, `order_status`, `created_at`, `updated_at`, `user_id`, `payment_reference`) VALUES (2, 'ORD-3267D4E3', 99.99, 'Elias Shumuye', 'eliasshumuye8@gmail.com', '0956037886', 'shjire', 'addis ababa', 'shire', '0000', 'cod', 1, 'delivered', '2026-05-29 14:23:25.301957', '2026-05-31 06:11:20.188238', 6, '');
INSERT INTO `orders_order` (`id`, `order_id`, `total_price`, `full_name`, `email`, `phone`, `shipping_address`, `city`, `state`, `zipcode`, `payment_method`, `payment_status`, `order_status`, `created_at`, `updated_at`, `user_id`, `payment_reference`) VALUES (3, 'ORD-5E3404D9', 449.97, 'Test User', 'test@example.com', '1234567890', '123 Test Street', 'Test City', 'Test State', '12345', 'card', 0, 'shipped', '2026-05-31 06:44:18.128349', '2026-06-02 08:30:21.342202', 2, '12345678vbn');
INSERT INTO `orders_order` (`id`, `order_id`, `total_price`, `full_name`, `email`, `phone`, `shipping_address`, `city`, `state`, `zipcode`, `payment_method`, `payment_status`, `order_status`, `created_at`, `updated_at`, `user_id`, `payment_reference`) VALUES (4, 'ORD-849B5407', 89.98, 'Test User', 'test@example.com', '1234567890', '123 Test Street', 'Test City', 'Test State', '12345', 'upi', 0, 'cancelled', '2026-05-31 19:18:16.406266', '2026-06-02 05:46:58.245235', 2, '1234567890');
INSERT INTO `orders_order` (`id`, `order_id`, `total_price`, `full_name`, `email`, `phone`, `shipping_address`, `city`, `state`, `zipcode`, `payment_method`, `payment_status`, `order_status`, `created_at`, `updated_at`, `user_id`, `payment_reference`) VALUES (5, 'ORD-6D42E681', 179.96, 'Test User', 'test@example.com', '1234567890', '123 Test Street', 'Test City', 'Test State', '12345', 'card', 0, 'delivered', '2026-06-02 07:36:06.386149', '2026-06-02 08:29:35.252932', 2, '345gcbl');
INSERT INTO `orders_order` (`id`, `order_id`, `total_price`, `full_name`, `email`, `phone`, `shipping_address`, `city`, `state`, `zipcode`, `payment_method`, `payment_status`, `order_status`, `created_at`, `updated_at`, `user_id`, `payment_reference`) VALUES (6, 'ORD-DE5C6D21', 39.99, 'Elias shumuye', 'eliasshumuyeela@gmail.com', '0970205815', 'SHIRE', 'shire', 'shire', '1234', 'card', 0, 'processing', '2026-06-07 09:00:43.827300', '2026-06-07 09:03:35.602498', 7, '123456789');


-- Table: orders_orderitem
DROP TABLE IF EXISTS `orders_orderitem`;
CREATE TABLE `orders_orderitem` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `quantity` INT NOT NULL,
  `price` DECIMAL(10,2) NOT NULL,
  `order_id` BIGINT NOT NULL,
  `product_id` BIGINT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: orders_orderitem
INSERT INTO `orders_orderitem` (`id`, `quantity`, `price`, `order_id`, `product_id`) VALUES (1, 6, 99.99, 1, 15);
INSERT INTO `orders_orderitem` (`id`, `quantity`, `price`, `order_id`, `product_id`) VALUES (2, 1, 99.99, 2, 15);
INSERT INTO `orders_orderitem` (`id`, `quantity`, `price`, `order_id`, `product_id`) VALUES (3, 3, 149.99, 3, 14);
INSERT INTO `orders_orderitem` (`id`, `quantity`, `price`, `order_id`, `product_id`) VALUES (4, 2, 44.99, 4, 143);
INSERT INTO `orders_orderitem` (`id`, `quantity`, `price`, `order_id`, `product_id`) VALUES (5, 3, 44.99, 5, 143);
INSERT INTO `orders_orderitem` (`id`, `quantity`, `price`, `order_id`, `product_id`) VALUES (6, 1, 44.99, 5, 110);
INSERT INTO `orders_orderitem` (`id`, `quantity`, `price`, `order_id`, `product_id`) VALUES (7, 1, 39.99, 6, 138);


-- Table: products_category
DROP TABLE IF EXISTS `products_category`;
CREATE TABLE `products_category` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `name` TEXT NOT NULL,
  `description` TEXT NOT NULL,
  `created_at` DATETIME NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: products_category
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (1, 'Electronics', 'Electronic devices and gadgets', '2026-05-29 09:48:09.033189');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (2, 'Clothing', 'Fashion and apparel', '2026-05-29 09:48:09.048160');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (3, 'Books', 'Books and literature', '2026-05-29 09:48:09.064721');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (4, 'Home & Kitchen', 'Home and kitchen items', '2026-05-29 09:48:09.083934');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (5, 'Fashion', 'Trendy clothing and accessories', '2026-05-31 10:03:47.965960');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (6, 'Sports & Fitness', 'Sports equipment and fitness gear', '2026-05-31 10:03:47.984736');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (7, 'Beauty & Personal Care', 'Beauty products and personal care items', '2026-05-31 10:03:48.001995');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (8, 'Toys & Games', 'Toys, games, and entertainment for all ages', '2026-05-31 10:03:48.016465');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (9, 'Automotive', 'Car accessories and automotive products', '2026-05-31 10:03:48.221423');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (10, 'Office Supplies', 'Office supplies and stationery', '2026-05-31 10:09:29.787635');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (11, 'Pet Supplies', 'Pet food, toys, and accessories', '2026-05-31 10:09:29.803002');
INSERT INTO `products_category` (`id`, `name`, `description`, `created_at`) VALUES (12, 'Garden & Outdoor', 'Gardening tools and outdoor equipment', '2026-05-31 10:09:29.817345');


-- Table: products_product
DROP TABLE IF EXISTS `products_product`;
CREATE TABLE `products_product` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `name` TEXT NOT NULL,
  `description` TEXT NOT NULL,
  `price` DECIMAL(10,2) NOT NULL,
  `image` TEXT NOT NULL,
  `stock` INT NOT NULL,
  `rating` DECIMAL(10,2) NOT NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  `category_id` BIGINT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: products_product
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (1, 'Wireless Headphones', 'Premium wireless headphones with noise cancellation and 30-hour battery life. Perfect for music lovers and professionals.', 99.99, 'products/product_1_KaTgSMW.jpg', 50, 4.5, '2026-05-29 09:48:09.099857', '2026-05-31 16:47:24.501552', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (2, 'Smart Watch', 'Feature-rich smartwatch with fitness tracking, heart rate monitor, and GPS. Stay connected on the go.', 199.99, 'products/product_2_9aAcUzu.jpg', 30, 4.7, '2026-05-29 09:48:09.115203', '2026-05-31 16:47:23.297386', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (3, 'Laptop Backpack', 'Durable laptop backpack with multiple compartments and USB charging port. Perfect for students and professionals.', 49.99, 'products/product_3_Dzi0he6.jpg', 75, 4.3, '2026-05-29 09:48:09.130605', '2026-05-31 16:47:22.350064', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (4, 'Bluetooth Speaker', 'Portable Bluetooth speaker with 360-degree sound and waterproof design. Great for outdoor activities.', 79.99, 'products/product_4_Ak1xqbs.jpg', 40, 4.6, '2026-05-29 09:48:09.145992', '2026-05-31 16:47:21.210757', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (5, 'Cotton T-Shirt', 'Comfortable 100% cotton t-shirt in various colors. Soft, breathable, and perfect for everyday wear.', 19.99, 'products/product_5_7LT2jnc.jpg', 100, 4.2, '2026-05-29 09:48:09.159837', '2026-05-31 16:47:19.984009', 2);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (6, 'Denim Jeans', 'Classic fit denim jeans with stretch comfort. Durable and stylish for any occasion.', 59.99, 'products/product_6_fEm5iy2.jpg', 80, 4.4, '2026-05-29 09:48:09.174163', '2026-05-31 16:47:18.919586', 2);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (7, 'Running Shoes', 'Lightweight running shoes with cushioned sole and breathable mesh. Perfect for athletes and fitness enthusiasts.', 89.99, 'products/product_7_Z9eyJ6V.jpg', 60, 4.8, '2026-05-29 09:48:09.194084', '2026-05-31 16:47:17.815473', 2);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (8, 'Winter Jacket', 'Warm winter jacket with water-resistant outer layer. Stay cozy in cold weather.', 129.99, 'products/product_8_Oucw7xt.jpg', 35, 4.5, '2026-05-29 09:48:09.211084', '2026-05-31 16:47:16.588851', 2);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (9, 'Python Programming', 'Complete guide to Python programming for beginners. Learn coding from scratch with practical examples.', 39.99, 'products/product_9_HzFMpuN.jpg', 60, 4.8, '2026-05-29 09:48:09.227501', '2026-05-31 16:47:15.371837', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (10, 'Web Development Book', 'Modern web development with HTML, CSS, and JavaScript. Build responsive websites from scratch.', 44.99, 'products/product_10_fPxRObS.jpg', 45, 4.6, '2026-05-29 09:48:09.243862', '2026-05-31 16:47:14.139497', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (11, 'Data Science Handbook', 'Comprehensive guide to data science with Python. Learn machine learning and data analysis.', 54.99, 'products/product_11_4RVZie7.jpg', 30, 4.9, '2026-05-29 09:48:09.259377', '2026-05-31 16:47:12.942008', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (12, 'Coffee Maker', 'Programmable coffee maker with thermal carafe. Brew perfect coffee every morning.', 79.99, 'products/product_12_4Cuqr5E.jpg', 40, 4.5, '2026-05-29 09:48:09.274934', '2026-05-31 16:47:11.741283', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (13, 'Blender', 'High-speed blender for smoothies and shakes. Powerful motor with multiple speed settings.', 69.99, 'products/product_13_Mgo2Kvu.jpg', 50, 4.4, '2026-05-29 09:48:09.290588', '2026-05-31 16:47:10.639346', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (14, 'Cookware Set', 'Non-stick cookware set with pots and pans. Complete kitchen solution for home chefs.', 149.99, 'products/product_14_4kGelV9.jpg', 22, 4.7, '2026-05-29 09:48:09.306101', '2026-05-31 16:47:09.533862', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (15, 'Air Fryer', 'Digital air fryer for healthy cooking. Fry, bake, and roast with little to no oil.', 99.99, 'products/product_15_AAVyL96.jpg', 28, 4.6, '2026-05-29 09:48:09.320784', '2026-05-31 16:47:08.364453', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (16, 'Wireless Bluetooth Earbuds Pro', 'Premium wireless earbuds with active noise cancellation, 30-hour battery life, and crystal-clear sound quality. Perfect for music lovers and professionals.', 79.99, 'products/product_16_PpXvtwS.jpg', 150, 4.8, '2026-05-31 10:03:48.239266', '2026-05-31 16:47:07.291575', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (17, 'Smart Watch Series 7', 'Advanced fitness tracker with heart rate monitor, GPS, sleep tracking, and 50+ sport modes. Water-resistant up to 50m.', 199.99, 'products/product_17_6qCdpqL.jpg', 85, 4.7, '2026-05-31 10:03:48.254704', '2026-05-31 16:47:06.232304', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (18, 'Portable Power Bank 20000mAh', 'High-capacity power bank with fast charging, dual USB ports, and LED display. Charges your phone 5-6 times.', 34.99, 'products/product_18_YCbT6FG.jpg', 200, 4.6, '2026-05-31 10:03:48.272114', '2026-05-31 16:47:05.257674', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (19, '4K Webcam with Ring Light', 'Professional 4K webcam with built-in ring light, auto-focus, and noise-canceling microphone. Perfect for streaming and video calls.', 89.99, 'products/product_19_OfghVef.jpg', 75, 4.5, '2026-05-31 10:03:48.289126', '2026-05-31 16:47:04.146475', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (20, 'Wireless Gaming Mouse RGB', 'High-precision gaming mouse with 16000 DPI, customizable RGB lighting, and 7 programmable buttons. Ergonomic design.', 49.99, 'products/product_20_Vmax7oF.jpg', 120, 4.7, '2026-05-31 10:03:48.305088', '2026-05-31 16:47:03.077157', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (21, 'USB-C Hub 7-in-1 Adapter', 'Multi-port USB-C hub with HDMI 4K, USB 3.0, SD card reader, and 100W power delivery. Compatible with all USB-C devices.', 39.99, 'products/product_21_HMVAKad.jpg', 180, 4.6, '2026-05-31 10:03:48.320888', '2026-05-31 16:47:02.003898', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (22, 'LED Desk Lamp with Wireless Charging', 'Modern LED desk lamp with adjustable brightness, color temperature control, and built-in wireless phone charger.', 45.99, 'products/product_22_WtKmyCv.jpg', 95, 4.5, '2026-05-31 10:03:48.336232', '2026-05-31 16:47:00.925007', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (23, 'Mechanical Gaming Keyboard RGB', 'Professional mechanical keyboard with blue switches, full RGB backlighting, and anti-ghosting. Perfect for gaming and typing.', 69.99, 'products/product_23_lHU748L.jpg', 110, 4.8, '2026-05-31 10:03:48.352305', '2026-05-31 16:46:59.863845', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (24, 'Premium Cotton T-Shirt Pack (3-Pack)', 'Soft, breathable 100% cotton t-shirts in classic colors. Pre-shrunk and machine washable. Available in S-XXL.', 29.99, 'products/product_24_JFZeexo.jpg', 250, 4.7, '2026-05-31 10:03:48.369545', '2026-05-31 16:46:58.685975', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (25, 'Slim Fit Denim Jeans', 'Stylish slim-fit jeans with stretch fabric for comfort. Classic 5-pocket design. Available in multiple washes.', 49.99, 'products/product_25_nr5FxDt.jpg', 180, 4.6, '2026-05-31 10:03:48.385740', '2026-05-31 16:46:57.630607', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (26, 'Leather Crossbody Bag', 'Genuine leather crossbody bag with adjustable strap and multiple compartments. Perfect for daily use.', 79.99, 'products/product_26_Dlfqamc.jpg', 90, 4.8, '2026-05-31 10:03:48.401707', '2026-05-31 16:46:56.530207', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (27, 'Casual Sneakers Unisex', 'Comfortable casual sneakers with cushioned insole and breathable mesh. Perfect for walking and everyday wear.', 59.99, 'products/product_27_MX997yx.jpg', 200, 4.5, '2026-05-31 10:03:48.418935', '2026-05-31 16:46:55.391144', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (28, 'Wool Blend Winter Coat', 'Warm and stylish wool blend coat with button closure and side pockets. Perfect for cold weather.', 129.99, 'products/product_28_PUfRGUX.jpg', 65, 4.7, '2026-05-31 10:03:48.435446', '2026-05-31 16:46:54.007336', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (29, 'Silk Scarf Collection', 'Luxurious 100% silk scarf with elegant patterns. Soft, lightweight, and versatile. Multiple colors available.', 34.99, 'products/product_29_McYrsno.jpg', 150, 4.6, '2026-05-31 10:03:48.452855', '2026-05-31 16:46:52.803507', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (30, 'Sports Watch Chronograph', 'Stylish sports watch with chronograph function, date display, and water resistance. Stainless steel case.', 89.99, 'products/product_30_TqLwWzg.jpg', 85, 4.5, '2026-05-31 10:03:48.468185', '2026-05-31 16:46:51.742400', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (31, 'Leather Belt Premium', 'Genuine leather belt with reversible design (black/brown). Durable buckle and adjustable length.', 39.99, 'products/product_31_pXeRTe4.jpg', 175, 4.7, '2026-05-31 10:03:48.482485', '2026-05-31 16:46:50.795502', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (32, 'Stainless Steel Cookware Set 10-Piece', 'Professional-grade stainless steel cookware set with non-stick coating. Includes pots, pans, and lids. Dishwasher safe.', 149.99, 'products/product_32_LbUcq02.jpg', 55, 4.8, '2026-05-31 10:03:48.497921', '2026-05-31 16:46:49.708147', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (33, 'Electric Kettle 1.7L Fast Boil', 'Rapid-boil electric kettle with auto shut-off, boil-dry protection, and cordless design. Boils water in 3 minutes.', 34.99, 'products/product_33_RmGeX7q.jpg', 140, 4.6, '2026-05-31 10:03:48.515322', '2026-05-31 16:46:48.611666', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (34, 'Memory Foam Pillow Set (2-Pack)', 'Ergonomic memory foam pillows with cooling gel and breathable cover. Perfect for side and back sleepers.', 49.99, 'products/product_34_616UxBk.jpg', 120, 4.7, '2026-05-31 10:03:48.532768', '2026-05-31 16:46:47.518589', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (35, 'Robot Vacuum Cleaner Smart', 'Intelligent robot vacuum with app control, auto-charging, and multiple cleaning modes. Works on all floor types.', 199.99, 'products/product_35_gGh5LCB.jpg', 45, 4.5, '2026-05-31 10:03:48.550097', '2026-05-31 16:46:46.575702', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (36, 'Air Fryer 5.5L Digital', 'Large capacity air fryer with digital controls, 8 preset programs, and non-stick basket. Healthy cooking made easy.', 89.99, 'products/product_36_1BCGGaO.jpg', 95, 4.8, '2026-05-31 10:03:48.566619', '2026-05-31 16:46:45.481724', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (37, 'Bamboo Cutting Board Set (3-Piece)', 'Eco-friendly bamboo cutting boards in 3 sizes. Knife-friendly, durable, and easy to clean.', 29.99, 'products/product_37_AneSFvV.jpg', 180, 4.6, '2026-05-31 10:03:48.584719', '2026-05-31 16:46:44.336839', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (38, 'LED String Lights 33ft', 'Warm white LED string lights with remote control, 8 lighting modes, and timer function. Perfect for decoration.', 19.99, 'products/product_38_IjqOJ06.jpg', 250, 4.7, '2026-05-31 10:03:48.604926', '2026-05-31 16:46:43.114549', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (39, 'Coffee Maker Programmable 12-Cup', 'Programmable coffee maker with auto-brew, pause-and-serve, and keep-warm function. Makes perfect coffee every time.', 59.99, 'products/product_39_8MRzIyc.jpg', 110, 4.5, '2026-05-31 10:03:48.620828', '2026-05-31 16:46:42.015600', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (40, 'Yoga Mat Extra Thick 10mm', 'Premium yoga mat with non-slip surface, extra cushioning, and carrying strap. Perfect for yoga, pilates, and stretching.', 34.99, 'products/product_40_YCR15Tc.jpg', 160, 4.7, '2026-05-31 10:03:48.637919', '2026-05-31 16:46:40.862574', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (41, 'Resistance Bands Set (5-Pack)', 'Professional resistance bands with 5 different resistance levels, handles, and door anchor. Complete home gym.', 24.99, 'products/product_41_qURSDg5.jpg', 200, 4.6, '2026-05-31 10:03:48.657700', '2026-05-31 16:46:39.764524', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (42, 'Adjustable Dumbbells 50lbs Pair', 'Space-saving adjustable dumbbells with quick-change weight system. Replaces 15 sets of weights.', 149.99, 'products/product_42_tJtU9AN.jpg', 65, 4.8, '2026-05-31 10:03:48.672535', '2026-05-31 16:46:38.544357', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (43, 'Jump Rope Speed with Counter', 'Professional speed jump rope with digital counter, adjustable length, and comfortable handles. Great for cardio.', 14.99, 'products/product_43_veNWvgF.jpg', 220, 4.5, '2026-05-31 10:03:48.686985', '2026-05-31 16:46:37.309016', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (44, 'Foam Roller for Muscle Recovery', 'High-density foam roller for deep tissue massage and muscle recovery. Includes exercise guide.', 29.99, 'products/product_44_pE40oxw.jpg', 145, 4.6, '2026-05-31 10:03:48.702452', '2026-05-31 16:46:36.122785', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (45, 'Sports Water Bottle 32oz Insulated', 'Stainless steel insulated water bottle keeps drinks cold for 24h or hot for 12h. Leak-proof and BPA-free.', 24.99, 'products/product_45_L3PHe52.jpg', 190, 4.7, '2026-05-31 10:03:48.718808', '2026-05-31 16:46:35.041136', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (46, 'Gym Bag Duffel with Shoe Compartment', 'Spacious gym bag with separate shoe compartment, wet pocket, and multiple pockets. Water-resistant material.', 39.99, 'products/product_46_dUfzG1D.jpg', 125, 4.5, '2026-05-31 10:03:48.733944', '2026-05-31 16:46:34.074890', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (47, 'Ab Roller Wheel with Knee Pad', 'Sturdy ab roller wheel with non-slip handles and thick knee pad. Perfect for core strengthening exercises.', 19.99, 'products/product_47_uiQUPAH.jpg', 175, 4.6, '2026-05-31 10:03:48.749648', '2026-05-31 16:46:32.992262', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (48, 'The Complete Guide to Python Programming', 'Comprehensive Python programming book for beginners to advanced. Includes practical examples and projects.', 39.99, 'products/product_48_kbwvvYo.jpg', 95, 4.8, '2026-05-31 10:03:48.766133', '2026-05-31 16:46:31.798691', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (49, 'Mindfulness and Meditation Handbook', 'Practical guide to mindfulness and meditation with daily exercises and techniques for stress relief.', 24.99, 'products/product_49_REJmuUp.jpg', 130, 4.7, '2026-05-31 10:03:48.782962', '2026-05-31 16:46:30.618396', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (50, 'Cookbook: Healthy Meals in 30 Minutes', '100+ quick and healthy recipes with nutritional information. Perfect for busy lifestyles.', 29.99, 'products/product_50_wtUoioY.jpg', 110, 4.6, '2026-05-31 10:03:48.801224', '2026-05-31 16:46:29.413825', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (51, 'Business Strategy and Leadership', 'Essential guide to business strategy, leadership skills, and organizational management.', 34.99, 'products/product_51_KBl22oG.jpg', 85, 4.5, '2026-05-31 10:03:48.818871', '2026-05-31 16:46:28.217029', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (52, 'Digital Photography Masterclass', 'Complete photography course covering camera settings, composition, lighting, and post-processing.', 44.99, 'products/product_52_7R5CeQx.jpg', 75, 4.8, '2026-05-31 10:03:48.834392', '2026-05-31 16:46:26.946081', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (53, 'Learn Spanish in 30 Days', 'Intensive Spanish language course with audio CD, exercises, and cultural insights.', 27.99, 'products/product_53_tdadn83.jpg', 120, 4.6, '2026-05-31 10:03:48.850340', '2026-05-31 16:46:25.745746', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (54, 'Vitamin C Serum Anti-Aging', 'Powerful vitamin C serum with hyaluronic acid for brightening, anti-aging, and hydration. Suitable for all skin types.', 29.99, 'products/product_54_nVLYzld.jpg', 180, 4.7, '2026-05-31 10:03:48.866283', '2026-05-31 16:46:24.581586', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (55, 'Electric Facial Cleansing Brush', 'Waterproof facial cleansing brush with 3 speed settings and soft silicone bristles. Deep cleans pores.', 39.99, 'products/product_55_awLhXm0.jpg', 140, 4.6, '2026-05-31 10:03:48.883384', '2026-05-31 16:46:23.367419', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (56, 'Hair Dryer Professional Ionic', 'Professional ionic hair dryer with 3 heat settings, cool shot button, and concentrator nozzle. Fast drying.', 49.99, 'products/product_56_DX6JkzK.jpg', 95, 4.5, '2026-05-31 10:03:48.899914', '2026-05-31 16:46:22.114889', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (57, 'Makeup Brush Set 15-Piece', 'Professional makeup brush set with synthetic bristles, ergonomic handles, and storage case.', 34.99, 'products/product_57_5EiRJAw.jpg', 160, 4.7, '2026-05-31 10:03:48.915551', '2026-05-31 16:46:21.002765', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (58, 'Natural Face Mask Set (5-Pack)', 'Variety pack of natural face masks for different skin concerns. Includes clay, charcoal, and hydrating masks.', 24.99, 'products/product_58_4nE2G9F.jpg', 200, 4.6, '2026-05-31 10:03:48.930745', '2026-05-31 16:46:19.753217', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (59, 'Electric Toothbrush Sonic', 'Sonic electric toothbrush with 5 cleaning modes, 2-minute timer, and 30-day battery life. Includes 4 brush heads.', 59.99, 'products/product_59_6UkZg4v.jpg', 115, 4.8, '2026-05-31 10:03:48.944069', '2026-05-31 16:46:18.662763', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (60, 'Nail Care Kit Professional', 'Complete nail care kit with electric nail file, cuticle pusher, and 6 attachments. Salon-quality manicure at home.', 29.99, 'products/product_60_FHCmyVI.jpg', 145, 4.5, '2026-05-31 10:03:48.959572', '2026-05-31 16:46:17.429191', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (61, 'Building Blocks Set 1000 Pieces', 'Creative building blocks set with 1000 colorful pieces. Compatible with major brands. Includes storage box.', 39.99, 'products/product_61_HAQwnYk.jpg', 125, 4.7, '2026-05-31 10:03:48.975591', '2026-05-31 16:46:16.319295', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (62, 'Remote Control Car 4WD Off-Road', 'High-speed RC car with 4-wheel drive, rechargeable battery, and durable design. Reaches 30mph.', 69.99, 'products/product_62_0nvWIwP.jpg', 85, 4.6, '2026-05-31 10:03:48.990305', '2026-05-31 16:46:15.062354', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (63, 'Educational STEM Robot Kit', 'Build and program your own robot. Includes 400+ parts and coding app. Perfect for ages 8+.', 89.99, 'products/product_63_Ut8dJcX.jpg', 65, 4.8, '2026-05-31 10:03:49.004789', '2026-05-31 16:46:13.997055', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (64, 'Board Game Family Collection', 'Classic board game collection including chess, checkers, backgammon, and more. Fun for all ages.', 34.99, 'products/product_64_EPHxVFe.jpg', 150, 4.5, '2026-05-31 10:03:49.020452', '2026-05-31 16:46:12.901898', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (65, 'Art Supplies Set for Kids', 'Complete art set with crayons, markers, colored pencils, paints, and paper. Includes carrying case.', 29.99, 'products/product_65_4Df0rzX.jpg', 180, 4.6, '2026-05-31 10:03:49.037899', '2026-05-31 16:46:11.806090', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (66, 'Puzzle 1000 Pieces Scenic', 'Beautiful scenic jigsaw puzzle with 1000 pieces. High-quality printing and precision-cut pieces.', 19.99, 'products/product_66_SVJsWHT.jpg', 200, 4.7, '2026-05-31 10:03:49.060718', '2026-05-31 16:46:10.639065', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (67, 'Car Phone Mount Magnetic', 'Strong magnetic car phone mount with 360° rotation and adjustable arm. Works with all phones.', 19.99, 'products/product_67_ZC93Ogh.jpg', 220, 4.6, '2026-05-31 10:03:49.091476', '2026-05-31 16:46:09.353223', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (68, 'Dash Cam 1080P Front and Rear', 'Dual dash cam with 1080P recording, night vision, G-sensor, and loop recording. Includes 32GB SD card.', 79.99, 'products/product_68_LRDOvFW.jpg', 95, 4.7, '2026-05-31 10:03:49.124495', '2026-05-31 16:46:08.129800', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (69, 'Car Vacuum Cleaner Portable', 'Powerful portable car vacuum with HEPA filter, LED light, and multiple attachments. Cordless design.', 44.99, 'products/product_69_v9Y92Oa.jpg', 130, 4.5, '2026-05-31 10:03:49.154672', '2026-05-31 16:46:06.948183', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (70, 'Tire Pressure Gauge Digital', 'Accurate digital tire pressure gauge with LCD display and auto shut-off. Essential for car maintenance.', 14.99, 'products/product_70_txBWC0E.jpg', 250, 4.6, '2026-05-31 10:03:49.176320', '2026-05-31 16:46:05.836080', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (71, 'Car Air Purifier Ionizer', 'Compact car air purifier with ionizer, removes odors, smoke, and allergens. USB powered.', 29.99, 'products/product_71_zg6k67v.jpg', 165, 4.5, '2026-05-31 10:03:49.221285', '2026-05-31 16:46:04.701285', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (72, 'Jump Starter Power Bank 12000mAh', 'Portable jump starter for cars with 12000mAh battery, LED flashlight, and USB charging ports.', 69.99, 'products/product_72_yq0c75l.jpg', 85, 4.8, '2026-05-31 10:03:49.243793', '2026-05-31 16:46:03.580920', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (73, 'Wireless Keyboard and Mouse Combo', 'Ergonomic wireless keyboard and mouse set with long battery life. Quiet keys and precise tracking.', 44.99, 'products/product_73_PN6vYAk.jpg', 135, 4.6, '2026-05-31 10:09:29.832443', '2026-05-31 16:46:02.497137', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (74, 'Laptop Stand Adjustable Aluminum', 'Ergonomic laptop stand with 6 adjustable heights. Improves posture and cooling. Fits 10-17 inch laptops.', 29.99, 'products/product_74_4AR3cNm.jpg', 170, 4.7, '2026-05-31 10:09:29.846822', '2026-05-31 16:46:01.303062', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (75, 'Bluetooth Speaker Waterproof', 'Portable Bluetooth speaker with 360° sound, 24-hour battery, and IPX7 waterproof rating.', 54.99, 'products/product_75_mPNjpWX.jpg', 145, 4.8, '2026-05-31 10:09:29.861757', '2026-05-31 16:46:00.017377', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (76, 'USB Flash Drive 128GB (3-Pack)', 'High-speed USB 3.0 flash drives with keychain design. Transfer speeds up to 100MB/s.', 24.99, 'products/product_76_IGhwlDW.jpg', 220, 4.5, '2026-05-31 10:09:29.876555', '2026-05-31 16:45:58.802700', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (77, 'HDMI Cable 4K 10ft (2-Pack)', 'Premium HDMI 2.0 cables supporting 4K@60Hz, HDR, and ARC. Gold-plated connectors.', 16.99, 'products/product_77_LxKwYhf.jpg', 280, 4.6, '2026-05-31 10:09:29.891833', '2026-05-31 16:45:57.694094', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (78, 'Phone Screen Protector Tempered Glass', 'Ultra-clear tempered glass screen protector with easy installation kit. 9H hardness.', 9.99, 'products/product_78_DDbTFIr.jpg', 350, 4.5, '2026-05-31 10:09:29.906982', '2026-05-31 16:45:56.606964', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (79, 'Laptop Cooling Pad with 6 Fans', 'Powerful cooling pad with 6 quiet fans, adjustable height, and dual USB ports.', 34.99, 'products/product_79_qpC4zv2.jpg', 115, 4.6, '2026-05-31 10:09:29.921488', '2026-05-31 16:45:55.292471', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (80, 'Smart LED Light Bulbs (4-Pack)', 'WiFi smart bulbs with 16 million colors, voice control, and scheduling. Works with Alexa.', 39.99, 'products/product_80_1DKeIxR.jpg', 160, 4.7, '2026-05-31 10:09:29.936549', '2026-05-31 16:45:54.257268', 1);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (81, 'Sunglasses Polarized UV Protection', 'Stylish polarized sunglasses with 100% UV protection. Lightweight and durable frame.', 24.99, 'products/product_81_gNjbtu5.jpg', 190, 4.6, '2026-05-31 10:09:29.950675', '2026-05-31 16:45:53.220309', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (82, 'Baseball Cap Adjustable Cotton', 'Classic baseball cap in 100% cotton with adjustable strap. Available in multiple colors.', 14.99, 'products/product_82_IKaTeY1.jpg', 240, 4.5, '2026-05-31 10:09:29.965054', '2026-05-31 16:45:52.263485', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (83, 'Backpack Laptop Travel 40L', 'Large capacity travel backpack with laptop compartment, USB charging port, and water-resistant material.', 49.99, 'products/product_83_DrmIgsu.jpg', 125, 4.7, '2026-05-31 10:09:29.980243', '2026-05-31 16:45:51.231980', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (84, 'Wallet Genuine Leather RFID Blocking', 'Slim leather wallet with RFID protection, multiple card slots, and ID window.', 29.99, 'products/product_84_5pdbtzQ.jpg', 175, 4.6, '2026-05-31 10:09:29.995655', '2026-05-31 16:45:50.188378', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (85, 'Socks Athletic Cushioned (6-Pack)', 'Comfortable athletic socks with arch support and moisture-wicking fabric. Fits shoe size 6-12.', 19.99, 'products/product_85_02TFmz7.jpg', 260, 4.5, '2026-05-31 10:09:30.012069', '2026-05-31 16:45:49.134676', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (86, 'Gloves Winter Touchscreen Compatible', 'Warm winter gloves with touchscreen fingertips. Soft fleece lining and anti-slip palm.', 16.99, 'products/product_86_4scPNvp.jpg', 200, 4.6, '2026-05-31 10:09:30.026703', '2026-05-31 16:45:47.927405', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (87, 'Tie Set with Pocket Square (3-Pack)', 'Classic silk ties with matching pocket squares. Perfect for business and formal occasions.', 34.99, 'products/product_87_X6nUiaV.jpg', 140, 4.7, '2026-05-31 10:09:30.043623', '2026-05-31 16:45:46.876636', 5);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (88, 'Blender High-Speed 1200W', 'Professional blender with 1200W motor, 6 blades, and 10 speed settings. Makes smoothies in seconds.', 79.99, 'products/product_88_nUmTDHS.jpg', 95, 4.7, '2026-05-31 10:09:30.059893', '2026-05-31 16:45:45.807256', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (89, 'Knife Set Kitchen 15-Piece', 'Professional knife set with stainless steel blades, ergonomic handles, and wooden block.', 69.99, 'products/product_89_HlV5TnZ.jpg', 110, 4.8, '2026-05-31 10:09:30.076095', '2026-05-31 16:45:44.687209', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (90, 'Microwave Oven 1.1 Cu.Ft', 'Compact microwave with 1000W power, 10 power levels, and express cooking buttons.', 89.99, 'products/product_90_ytLJ2OD.jpg', 75, 4.5, '2026-05-31 10:09:30.091420', '2026-05-31 16:45:43.609207', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (91, 'Dish Drying Rack Stainless Steel', 'Large capacity dish rack with utensil holder, cutting board slot, and drip tray.', 34.99, 'products/product_91_0Gge6KL.jpg', 155, 4.6, '2026-05-31 10:09:30.105975', '2026-05-31 16:45:42.537141', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (92, 'Food Storage Containers Set (20-Piece)', 'BPA-free plastic containers with snap-lock lids. Microwave, freezer, and dishwasher safe.', 29.99, 'products/product_92_634uGvd.jpg', 185, 4.7, '2026-05-31 10:09:30.127054', '2026-05-31 16:45:41.482099', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (93, 'Shower Curtain Waterproof with Hooks', 'Mildew-resistant shower curtain with reinforced holes and 12 rust-proof hooks.', 19.99, 'products/product_93_ctNPUQn.jpg', 210, 4.5, '2026-05-31 10:09:30.142524', '2026-05-31 16:45:40.385648', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (94, 'Bath Towel Set 6-Piece Egyptian Cotton', 'Luxury Egyptian cotton towels - 2 bath, 2 hand, 2 face towels. Super soft and absorbent.', 44.99, 'products/product_94_Lsb54l0.jpg', 130, 4.8, '2026-05-31 10:09:30.158489', '2026-05-31 16:45:39.316366', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (95, 'Trash Can Touchless 13 Gallon', 'Automatic sensor trash can with soft-close lid, fingerprint-proof finish, and odor filter.', 59.99, 'products/product_95_H3kPPXq.jpg', 85, 4.6, '2026-05-31 10:09:30.174719', '2026-05-31 16:45:38.214675', 4);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (96, 'Treadmill Folding Electric', 'Compact folding treadmill with LCD display, 12 programs, and 300lb weight capacity.', 299.99, 'products/product_96_5lD6wa3.jpg', 35, 4.7, '2026-05-31 10:09:30.192461', '2026-05-31 16:45:37.101837', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (97, 'Exercise Bike Stationary Indoor', 'Quiet magnetic resistance bike with adjustable seat, LCD monitor, and tablet holder.', 199.99, 'products/product_97_pp5nbvV.jpg', 55, 4.6, '2026-05-31 10:09:30.208819', '2026-05-31 16:45:36.017165', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (98, 'Kettlebell Set 3-Piece (10, 15, 20 lbs)', 'Vinyl-coated kettlebells with wide handles. Perfect for strength training and cardio.', 79.99, 'products/product_98_el0u07l.jpg', 95, 4.7, '2026-05-31 10:09:30.224301', '2026-05-31 16:45:34.798800', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (99, 'Pull-Up Bar Doorway Mounted', 'Heavy-duty pull-up bar with multiple grip positions. No screws needed, fits most doorways.', 29.99, 'products/product_99_iz39fzQ.jpg', 165, 4.5, '2026-05-31 10:09:30.238526', '2026-05-31 16:45:33.707317', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (100, 'Ankle Weights Adjustable (Pair)', 'Comfortable ankle weights with adjustable straps. Each weight adjustable from 1-5 lbs.', 24.99, 'products/product_100_d58E0bh.jpg', 180, 4.6, '2026-05-31 10:09:30.253240', '2026-05-31 16:45:32.507002', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (101, 'Yoga Block and Strap Set', 'High-density foam yoga blocks (2-pack) with 8ft cotton strap. Perfect for beginners.', 19.99, 'products/product_101_Js4jwwI.jpg', 200, 4.5, '2026-05-31 10:09:30.267290', '2026-05-31 16:45:31.298496', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (102, 'Fitness Tracker Smart Band', 'Activity tracker with heart rate monitor, sleep tracking, and 14-day battery life.', 39.99, 'products/product_102_mLmnbAI.jpg', 145, 4.6, '2026-05-31 10:09:30.281096', '2026-05-31 16:45:30.238475', 6);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (103, 'Web Development Complete Course', 'Learn HTML, CSS, JavaScript, React, and Node.js. Includes 50+ projects and exercises.', 49.99, 'products/product_103_iMpglA2.jpg', 85, 4.8, '2026-05-31 10:09:30.296239', '2026-05-31 16:45:29.051041', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (104, 'Financial Freedom Guide', 'Practical guide to investing, saving, and building wealth. Includes worksheets and calculators.', 29.99, 'products/product_104_U1MVOyi.jpg', 115, 4.7, '2026-05-31 10:09:30.311298', '2026-05-31 16:45:27.858929', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (105, 'Gardening for Beginners', 'Complete guide to growing vegetables, herbs, and flowers. Includes planting calendar.', 24.99, 'products/product_105_aKieA2H.jpg', 130, 4.6, '2026-05-31 10:09:30.325893', '2026-05-31 16:45:26.659698', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (106, 'Drawing and Sketching Masterclass', 'Learn to draw portraits, landscapes, and still life. Step-by-step tutorials included.', 34.99, 'products/product_106_bMgrMpR.jpg', 95, 4.7, '2026-05-31 10:09:30.341336', '2026-05-31 16:45:25.468665', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (107, 'Yoga and Meditation Guide', 'Comprehensive yoga guide with 100+ poses, breathing exercises, and meditation techniques.', 27.99, 'products/product_107_IML7gSi.jpg', 120, 4.6, '2026-05-31 10:09:30.356619', '2026-05-31 16:45:24.292211', 3);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (108, 'Retinol Cream Anti-Aging Night', 'Powerful retinol night cream with hyaluronic acid. Reduces wrinkles and fine lines.', 34.99, 'products/product_108_CbF95NS.jpg', 155, 4.7, '2026-05-31 10:09:30.372064', '2026-05-31 16:45:23.112586', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (109, 'Jade Roller and Gua Sha Set', 'Natural jade facial roller and gua sha tool for lymphatic drainage and facial massage.', 19.99, 'products/product_109_fVc1oED.jpg', 185, 4.6, '2026-05-31 10:09:30.388523', '2026-05-31 16:45:21.931574', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (110, 'Hair Straightener Ceramic Flat Iron', 'Professional ceramic flat iron with adjustable temperature, auto shut-off, and dual voltage.', 44.99, 'products/product_110_TBqpzrD.jpg', 124, 4.7, '2026-05-31 10:09:30.405369', '2026-06-02 07:36:06.445081', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (111, 'Perfume Gift Set Women (3-Pack)', 'Luxury perfume collection with floral, fruity, and oriental scents. 30ml each bottle.', 39.99, 'products/product_111_4Pkxicb.jpg', 140, 4.5, '2026-05-31 10:09:30.420935', '2026-05-31 16:45:19.675557', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (112, 'Manicure Pedicure Kit 18-Piece', 'Professional nail care kit with stainless steel tools and leather case.', 24.99, 'products/product_112_GThfHXn.jpg', 170, 4.6, '2026-05-31 10:09:30.437547', '2026-05-31 16:45:18.616930', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (113, 'Body Lotion Moisturizing 16oz', 'Hydrating body lotion with shea butter and vitamin E. Non-greasy formula.', 16.99, 'products/product_113_kt2Z9JM.jpg', 210, 4.5, '2026-05-31 10:09:30.453029', '2026-05-31 16:45:17.561105', 7);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (114, 'LEGO Compatible Building Set 500 Pieces', 'Creative building blocks with instruction booklet. Compatible with major brands.', 29.99, 'products/product_114_3tokMmh.jpg', 155, 4.6, '2026-05-31 10:09:30.467841', '2026-05-31 16:45:16.370968', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (115, 'Drone with Camera 1080P', 'Beginner-friendly drone with HD camera, altitude hold, and one-key takeoff/landing.', 89.99, 'products/product_115_G89F09C.jpg', 75, 4.7, '2026-05-31 10:09:30.482769', '2026-05-31 16:45:15.135997', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (116, 'Coloring Books for Adults (4-Pack)', 'Stress-relief coloring books with intricate designs. Includes mandala, nature, and patterns.', 19.99, 'products/product_116_Ite6oXV.jpg', 190, 4.5, '2026-05-31 10:09:30.496773', '2026-05-31 16:45:14.101892', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (117, 'Playing Cards Deck Set (12-Pack)', 'Standard playing cards in bulk. Perfect for parties, games, and magic tricks.', 14.99, 'products/product_117_KOJIhLj.jpg', 240, 4.6, '2026-05-31 10:09:30.511243', '2026-05-31 16:45:12.910333', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (118, 'Stuffed Animal Plush Teddy Bear', 'Soft and cuddly teddy bear, 18 inches tall. Hypoallergenic and machine washable.', 24.99, 'products/product_118_OACOFsu.jpg', 165, 4.7, '2026-05-31 10:09:30.525872', '2026-05-31 16:45:11.874311', 8);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (119, 'Car Seat Covers Full Set', 'Universal fit car seat covers with airbag compatible design. Easy to install and clean.', 49.99, 'products/product_119_vcE1T55.jpg', 105, 4.6, '2026-05-31 10:09:30.546171', '2026-05-31 16:45:10.616692', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (120, 'Car Floor Mats All-Weather (4-Piece)', 'Heavy-duty rubber floor mats with raised edges. Protects from mud, snow, and spills.', 39.99, 'products/product_120_MlY3Tk9.jpg', 135, 4.7, '2026-05-31 10:09:30.562740', '2026-05-31 16:45:09.548554', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (121, 'Windshield Sun Shade Reflective', 'Foldable sun shade keeps car cool. Fits most vehicles, easy to install and store.', 12.99, 'products/product_121_OVRu0lX.jpg', 230, 4.5, '2026-05-31 10:09:30.579604', '2026-05-31 16:45:08.465092', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (122, 'Car Organizer Trunk Storage', 'Collapsible trunk organizer with multiple compartments and non-slip bottom.', 24.99, 'products/product_122_0WS95qL.jpg', 175, 4.6, '2026-05-31 10:09:30.595616', '2026-05-31 16:45:07.354805', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (123, 'LED Headlight Bulbs H11 (Pair)', 'Super bright LED headlight bulbs, 6000K white light, 50,000 hour lifespan.', 34.99, 'products/product_123_2RkUqiL.jpg', 145, 4.7, '2026-05-31 10:09:30.611200', '2026-05-31 16:45:06.161191', 9);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (124, 'Office Chair Ergonomic Mesh', 'Comfortable mesh office chair with lumbar support, adjustable height, and armrests.', 129.99, 'products/product_124_3T9JgkF.jpg', 65, 4.7, '2026-05-31 10:09:30.629231', '2026-05-31 16:45:05.092908', 10);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (125, 'Desk Organizer Set 5-Piece', 'Bamboo desk organizer with pen holder, paper tray, and drawer. Keeps desk tidy.', 29.99, 'products/product_125_YCPpa1R.jpg', 155, 4.6, '2026-05-31 10:09:30.646062', '2026-05-31 16:45:03.991653', 10);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (126, 'Printer Paper 500 Sheets', 'Bright white printer paper, 8.5x11 inches, 20lb weight. Jam-free performance.', 9.99, 'products/product_126_wZerRJW.jpg', 300, 4.5, '2026-05-31 10:09:30.662753', '2026-05-31 16:45:02.831896', 10);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (127, 'Sticky Notes Assorted Colors (12-Pack)', 'Colorful sticky notes in various sizes. 100 sheets per pad, strong adhesive.', 12.99, 'products/product_127_acPTcvf.jpg', 250, 4.6, '2026-05-31 10:09:30.680431', '2026-05-31 16:45:01.796198', 10);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (128, 'Stapler Heavy Duty with Staples', 'Professional stapler staples up to 50 sheets. Includes 5000 staples.', 16.99, 'products/product_128_M9WZdPf.jpg', 200, 4.5, '2026-05-31 10:09:30.696775', '2026-05-31 16:45:00.762073', 10);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (129, 'Whiteboard Magnetic 36x24 inches', 'Dry erase whiteboard with aluminum frame, marker tray, and mounting hardware.', 39.99, 'products/product_129_UIeHXOm.jpg', 95, 4.7, '2026-05-31 10:09:30.713098', '2026-05-31 16:44:59.557261', 10);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (130, 'File Folders Letter Size (100-Pack)', 'Manila file folders with 1/3 cut tabs. Durable and acid-free.', 14.99, 'products/product_130_izujvFo.jpg', 220, 4.5, '2026-05-31 10:09:30.728499', '2026-05-31 16:44:58.483119', 10);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (131, 'Dog Food Dry Premium 30lbs', 'High-protein dog food with real chicken, vegetables, and no artificial flavors.', 49.99, 'products/product_131_8WA5ASX.jpg', 85, 4.8, '2026-05-31 10:09:30.744708', '2026-05-31 16:44:57.437447', 11);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (132, 'Cat Litter Box Self-Cleaning', 'Automatic self-cleaning litter box with odor control and waste drawer.', 149.99, 'products/product_132_uchmwGH.jpg', 45, 4.6, '2026-05-31 10:09:30.763005', '2026-05-31 16:44:56.349360', 11);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (133, 'Pet Carrier Airline Approved', 'Soft-sided pet carrier with ventilation, shoulder strap, and storage pocket.', 34.99, 'products/product_133_9fs7V6c.jpg', 125, 4.7, '2026-05-31 10:09:30.777559', '2026-05-31 16:44:55.146442', 11);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (134, 'Dog Leash and Collar Set', 'Durable nylon leash (6ft) and adjustable collar with reflective stitching.', 19.99, 'products/product_134_mdlKUwa.jpg', 180, 4.5, '2026-05-31 10:09:30.793462', '2026-05-31 16:44:54.102703', 11);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (135, 'Cat Scratching Post with Toys', 'Tall scratching post covered in sisal rope with hanging toys and platform.', 39.99, 'products/product_135_35whQr6.jpg', 110, 4.6, '2026-05-31 10:09:30.810153', '2026-05-31 16:44:53.032521', 11);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (136, 'Pet Water Fountain 2L', 'Automatic pet water fountain with filter, quiet pump, and LED light.', 29.99, 'products/product_136_GvHUwXK.jpg', 145, 4.7, '2026-05-31 10:09:30.825130', '2026-05-31 16:44:51.771958', 11);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (137, 'Dog Toys Variety Pack (10-Piece)', 'Assorted dog toys including rope, balls, and squeaky toys. Durable and safe.', 24.99, 'products/product_137_O2a3Qkf.jpg', 165, 4.6, '2026-05-31 10:09:30.838779', '2026-05-31 16:44:50.554705', 11);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (138, 'Garden Tool Set 10-Piece', 'Complete gardening tool set with trowel, pruner, rake, and carrying bag.', 39.99, 'products/product_138_ZJRoT8W.jpg', 114, 4.7, '2026-05-31 10:09:30.853741', '2026-06-07 09:00:43.873398', 12);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (139, 'Garden Hose 50ft Expandable', 'Lightweight expandable hose with 9-pattern spray nozzle. No kinks or tangles.', 29.99, 'products/product_139_nfBl6aY.jpg', 140, 4.6, '2026-05-31 10:09:30.868002', '2026-05-31 16:44:48.093415', 12);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (140, 'Plant Pots Ceramic Set (6-Pack)', 'Decorative ceramic pots with drainage holes and saucers. Various sizes.', 34.99, 'products/product_140_MdkvqFX.jpg', 125, 4.5, '2026-05-31 10:09:30.882075', '2026-05-31 16:44:46.914731', 12);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (141, 'Solar Garden Lights (12-Pack)', 'Waterproof solar pathway lights with auto on/off. No wiring needed.', 39.99, 'products/product_141_bIi2chI.jpg', 155, 4.7, '2026-05-31 10:09:30.896672', '2026-05-31 16:44:45.728241', 12);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (142, 'Lawn Sprinkler Oscillating', 'Adjustable sprinkler covers up to 3600 sq ft. Durable metal construction.', 24.99, 'products/product_142_Fa5EbTX.jpg', 175, 4.6, '2026-05-31 10:09:30.911575', '2026-06-02 07:49:07.834471', 12);
INSERT INTO `products_product` (`id`, `name`, `description`, `price`, `image`, `stock`, `rating`, `created_at`, `updated_at`, `category_id`) VALUES (143, 'Garden Kneeler and Seat', 'Foldable garden kneeler with tool pouches. Converts to seat. Supports 330lbs.', 44.99, 'products/product_143_gkcHpqG.jpg', 90, 4.7, '2026-05-31 10:09:30.926815', '2026-06-02 07:36:06.417250', 12);


-- Table: products_productreview
DROP TABLE IF EXISTS `products_productreview`;
CREATE TABLE `products_productreview` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `rating` INT NOT NULL,
  `comment` TEXT NOT NULL,
  `created_at` DATETIME NOT NULL,
  `product_id` BIGINT NOT NULL,
  `user_id` INT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Table: users_announcement
DROP TABLE IF EXISTS `users_announcement`;
CREATE TABLE `users_announcement` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `title` TEXT NOT NULL,
  `content` TEXT NOT NULL,
  `created_at` DATETIME NOT NULL,
  `is_active` TINYINT(1) NOT NULL,
  `category` TEXT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: users_announcement
INSERT INTO `users_announcement` (`id`, `title`, `content`, `created_at`, `is_active`, `category`) VALUES (1, '🛒 New Order #5 from testuser', 'Customer testuser placed an order for $179.96. Order contains 2 item(s). Payment method: Credit/Debit Card. Please check the order management page.', '2026-06-02 07:36:06.461530', 1, 'order');
INSERT INTO `users_announcement` (`id`, `title`, `content`, `created_at`, `is_active`, `category`) VALUES (2, '🛒 New Order #6 from ella12', 'Customer ella12 placed an order for $39.99. Order contains 1 item(s). Payment method: Credit/Debit Card. Please check the order management page.', '2026-06-07 09:00:43.891823', 1, 'order');


-- Table: users_complaint
DROP TABLE IF EXISTS `users_complaint`;
CREATE TABLE `users_complaint` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `subject` TEXT NOT NULL,
  `message` TEXT NOT NULL,
  `image` TEXT,
  `status` TEXT NOT NULL,
  `admin_reply` TEXT NOT NULL,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  `replied_at` DATETIME,
  `user_id` INT NOT NULL,
  `is_urgent` TINYINT(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: users_complaint
INSERT INTO `users_complaint` (`id`, `subject`, `message`, `image`, `status`, `admin_reply`, `created_at`, `updated_at`, `replied_at`, `user_id`, `is_urgent`) VALUES (1, 'is not trye', 'xbjyearsdqj3yg', 'complaints/Screenshot_231.png', 'pending', '', '2026-05-29 14:22:40.771346', '2026-05-29 14:22:40.771429', NULL, 6, 0);


-- Table: users_contactmessage
DROP TABLE IF EXISTS `users_contactmessage`;
CREATE TABLE `users_contactmessage` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `name` TEXT NOT NULL,
  `email` TEXT NOT NULL,
  `subject` TEXT NOT NULL,
  `message` TEXT NOT NULL,
  `created_at` DATETIME NOT NULL,
  `is_read` TINYINT(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Table: users_notification
DROP TABLE IF EXISTS `users_notification`;
CREATE TABLE `users_notification` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `notification_type` TEXT NOT NULL,
  `title` TEXT NOT NULL,
  `message` TEXT NOT NULL,
  `link` TEXT NOT NULL,
  `is_read` TINYINT(1) NOT NULL,
  `created_at` DATETIME NOT NULL,
  `user_id` INT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Table: users_userprofile
DROP TABLE IF EXISTS `users_userprofile`;
CREATE TABLE `users_userprofile` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `phone` TEXT NOT NULL,
  `address` TEXT NOT NULL,
  `city` TEXT NOT NULL,
  `state` TEXT NOT NULL,
  `zipcode` TEXT NOT NULL,
  `created_at` DATETIME NOT NULL,
  `user_id` INT NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Data for table: users_userprofile
INSERT INTO `users_userprofile` (`id`, `phone`, `address`, `city`, `state`, `zipcode`, `created_at`, `user_id`) VALUES (1, '1234567890', '123 Test Street', 'Test City', 'Test State', '12345', '2026-05-29 09:59:00.543691', 2);
INSERT INTO `users_userprofile` (`id`, `phone`, `address`, `city`, `state`, `zipcode`, `created_at`, `user_id`) VALUES (2, '1234567890', '123 Test Street', 'Test City', 'Test State', '12345', '2026-05-29 09:59:02.178214', 3);
INSERT INTO `users_userprofile` (`id`, `phone`, `address`, `city`, `state`, `zipcode`, `created_at`, `user_id`) VALUES (3, '1234567890', '123 Test Street', 'Test City', 'Test State', '12345', '2026-05-29 09:59:03.639040', 4);
INSERT INTO `users_userprofile` (`id`, `phone`, `address`, `city`, `state`, `zipcode`, `created_at`, `user_id`) VALUES (4, '1234567890', '123 Test Street', 'Test City', 'Test State', '12345', '2026-05-29 09:59:05.182130', 5);
INSERT INTO `users_userprofile` (`id`, `phone`, `address`, `city`, `state`, `zipcode`, `created_at`, `user_id`) VALUES (5, '0956037886', 'aksum', 'addis ababa', '', '0000', '2026-05-29 10:52:23.539522', 1);
INSERT INTO `users_userprofile` (`id`, `phone`, `address`, `city`, `state`, `zipcode`, `created_at`, `user_id`) VALUES (6, '', '', '', '', '', '2026-05-29 12:54:54.779359', 6);
INSERT INTO `users_userprofile` (`id`, `phone`, `address`, `city`, `state`, `zipcode`, `created_at`, `user_id`) VALUES (7, '', '', '', '', '', '2026-06-07 08:57:21.390099', 7);


-- Table: users_userreport
DROP TABLE IF EXISTS `users_userreport`;
CREATE TABLE `users_userreport` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `username` TEXT NOT NULL,
  `email` TEXT NOT NULL,
  `report_type` TEXT NOT NULL,
  `message` TEXT NOT NULL,
  `created_at` DATETIME NOT NULL,
  `is_resolved` TINYINT(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

