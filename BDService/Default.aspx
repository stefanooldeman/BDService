<%@ Page Language="C#" Inherits="BDService.Default" %>

<html>
<head>
<style>
	ul li span { margin-left: 20px; }
</style>
</head>
<body>
	<h1>Overview of Services (endpoints)</h1>
	<ul>
		<li><a href="/v1/user">/v1/user</a><span>create a user, manage a user by credentials</span></li>
		<li><a href="/v1/user/yourusername/cart">/v1/user/yourname/cart</a><span>add a product, manage your cart by credentials</span></li>
		<li><a href="/v1/products">/v1/products</a><span>view our catalog of products, order</span></li>
		<!--- <li><a href="/v1/stock">/v1/stock</a><span>manage the stores stock</span></li> -->
	</ul>
</body>
</html>

<!-- 
{"services": {
	{endpoint: {uri: "/v1/hello", description: "hello world endpoint"},
	{endpoint: {uri: "/v1/products", description: "view our catalog of products, order"}
	{endpoint: {uri: "/v1/stock", description: "manage the stores stock"}
	{endpoint: {uri: "/v1/user", description: "create a user, manage a user by credentials"}
}}
-->