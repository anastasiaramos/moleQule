<!DOCTYPE html>
<html>
  <head>
    <meta charset="utf-8">
    <title>Fine Uploader - jQuery Wrapper Minimal Demo</title>
    
	<link rel="icon" href="./Content/themes/qapture/img/favicon.ico" type="image/x-icon">
	<link rel="shortcut icon" href="./Content/themes/qapture/img/favicon.ico" type="image/x-icon" />    
	<link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
	
	<!-- Main Theme Stylesheet :: CSS -->
	<link href="./Content/themes/qapture/css/style.css" rel="stylesheet"/>
	<link href="./Content/themes/qapture/css/style-responsive.css" rel="stylesheet"/>
	<link href="./Content/themes/qapture/css/prettify.css" rel="stylesheet"/>
	<link href="./Content/themes/qapture/css/tm_docs.css" rel="stylesheet"/>
	<link href="./Content/themes/qapture/css/bootstrap-formhelpers-countries.flags.css" rel="stylesheet"/>
	<link href="./Content/themes/qapture/css/fineuploader.css" rel="stylesheet"/>

	<link href="./Content/shared/fonts/glyphicons/css/glyphicons.css" rel="stylesheet"/>

	
	<!-- Le HTML5 shim, for IE6-8 support of HTML5 elements -->
	<!--[if lt IE 9]>
	  <script src="/scripts/html5shiv.js"></script>
	<![endif]-->

	<script src="./Scripts/shared/jquery-1.9.1.js"></script>
	
  </head>
  <body>
  
    <div id="jquery-wrapped-fine-uploader"></div>

	<!--<script src="./Scripts/shared/silverlight.js"></script>
	<script src="./Scripts/shared/silverlight.error.js"></script>-->

	<!--<script src="./Scripts/shared/canvas-to-blob.min.js" type="text/javascript"></script>-->
	<script src="./Scripts/shared/jquery.fineuploader-3.7.1.js" type="text/javascript"></script>
	
    <script>
      $(document).ready(function () {
        $('#jquery-wrapped-fine-uploader').fineUploader({
          request: {
            endpoint: 'Endpoint.php'
          }
        });
      });
    </script>
	
	<script src="./Scripts/shared/jquery.unobtrusive-ajax.js"></script>
	<script src="./Scripts/shared/jquery.validate.js"></script>
	<script src="./Scripts/shared/jquery.validate.unobtrusive-custom-for-bootstrap.js"></script>
	<script src="./Scripts/shared/jquery.validate.unobtrusive.js"></script>

	<script src="./Scripts/shared/bootstrap.js"></script>

	<script src="./Scripts/themes/base/prettify.js"></script>
	<script src="./Scripts/themes/base/holder.js"></script>
	<script src="./Scripts/themes/base/application.js"></script>
	<script src="./Scripts/themes/base/jquery.cookie.js"></script>
	<script src="./Scripts/themes/base/superfish.js"></script>
	<script src="./Scripts/themes/base/jquery.mobilemenu.js"></script>
	<script src="./Scripts/themes/base/jquery.easing.1.3.js"></script>
	<script src="./Scripts/themes/base/jquery.tweet.js"></script>
	<script src="./Scripts/themes/base/molSearches.js"></script>
	
  </body>
</html>