<?php
// 10.11.2020
// Own project

// Function to get the text between two strings
function get_string_between($string, $start, $end){
    $string = ' ' . $string;
    $ini = strpos($string, $start);
    if ($ini == 0) return '';
    $ini += strlen($start);
    $len = strpos($string, $end, $ini) - $ini;
    return substr($string, $ini, $len);
}

// Get all image links in the html that match the regular expression
function getContents($str, $startDelimiter, $endDelimiter) {
  $contents = array();
  $startDelimiterLength = strlen($startDelimiter);
  $endDelimiterLength = strlen($endDelimiter);
  $startFrom = $contentStart = $contentEnd = 0;
  while (false !== ($contentStart = strpos($str, $startDelimiter, $startFrom))) {
    $contentStart += $startDelimiterLength;
    $contentEnd = strpos($str, $endDelimiter, $contentStart);
    if (false === $contentEnd) {
      break;
    }
    $contents[] = substr($str, $contentStart, $contentEnd - $contentStart);
    $startFrom = $contentEnd + $endDelimiterLength;
  }

  return $contents;
}

// Function to print out a string in the screen
function output($str) {
    echo $str;
    ob_end_flush();
    flush();
    ob_start();
}
set_time_limit(20000000000);






// Startin index and amount of pages to process
$index = 95000;
$iMax = $index + 40;

// While the index is smaller than the max_index: 
while($index < $iMax){
echo "Downloading: $index<br>";




$url = "https://XXXXXXXX.com/story/".str_pad($index, 5, "0", STR_PAD_LEFT)."/attachments/photos-videos";


if(isset($_POST['url'])){
    
    $url = $_POST['url'];
    
}

// Get the HTML from the current URL, Get all image urls from the HTML, local folder name "Batch"
$html = file_get_contents($url);
$allUrls = getContents($html, '<img class="lazy" src="', '?fit=crop&');
$category = 'Batch';




// Get the Architecture Project Name and sanitize it
$title = get_string_between($html, '<meta property="og:title" content="', '| Media'); 
$title = htmlspecialchars($title);

$title = str_replace('|','',$title);
$title = str_replace('\\','',$title);
$title = str_replace('/','',$title);
$title = str_replace(';','',$title);
$title = str_replace('&amp','',$title);
$title = str_replace('amp','',$title);
$title = str_replace(',','',$title);
$title = str_replace('  ',' ',$title);
$title = str_replace(' ','-',$title);


// Display the title
echo $title."<br>";


// Check if folder does NOT exist, then create it
if (!file_exists("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/")) {
    mkdir("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/", 0777, true);
}


// For each URL
for($i = 0; $i < count($allUrls); $i ++){
    sleep(2);
	// Download the image and store it in the local folder, if an error occurs, display an error message
    $content = file_get_contents(str_replace('https://XXXXXXXX.com/thumbs', 'https://XXXXXXXX.s3.eu-central-1.amazonaws.com', $allUrls[$i]));
    if(file_put_contents("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/".($i+1).".jpg", $content)){

            output("$allUrls[$i] downloaded <br>");
        
    }else{
        
            output("Failed $allUrls[$i] downloaded <br>"); 
        
    }

}


// Check if the project has Renders or Drawings
$rendersAndDrawings = get_string_between($html, ' <ul id="nav-gallery" class="nav-pills nav"><li class="active">', '<div class="section section-grid-extended pt-sm-6">'); 

if(strpos($rendersAndDrawings, 'Renders')!== false){
                $hasRenders = 1;
        }else{$hasRenders = 0;}
		
echo "HAS RENDERS: $hasRenders<br>";


if(strpos($rendersAndDrawings, 'Drawings')!== false){
                $hasDrawings = 1;
        }else{$hasDrawings = 0;}

echo "HAS Drawings: $hasDrawings<br>";


// If it has Drawings, proceed to download and store them in the local folder
if($hasDrawings == 1){
    
    

                $urlDrawings = str_replace('photos-videos','drawings',$url);
                    sleep(2);
                $htmlDrawings = file_get_contents($urlDrawings);

                $allUrls = getContents($htmlDrawings, '<img class="lazy" src="', '?fit=crop&');

                var_dump($allUrls);

                $title = get_string_between($html, '<meta property="og:title" content="', '| Media'); 

                $title = htmlspecialchars($title);
				
                $title = str_replace('|','',$title);
                $title = str_replace('\\','',$title);
                $title = str_replace('/','',$title);
                $title = str_replace(';','',$title);
                $title = str_replace('&amp','',$title);
                $title = str_replace('amp','',$title);
                $title = str_replace(',','',$title);
                $title = str_replace('  ',' ',$title);
                $title = str_replace(' ','-',$title);

                echo $title;

                if (!file_exists("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/drawings")) {
                    mkdir("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/drawings", 0777, true);
                }
    
    
                for($i = 0; $i < count($allUrls); $i ++){
                    sleep(2);
                    $content = file_get_contents(str_replace('https://XXXXXXXX.com/thumbs', 'https://XXXXXXXX.s3.eu-central-1.amazonaws.com', $allUrls[$i]));
                    
                    if(file_put_contents("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/drawings/".($i+1).".jpg", $content)){

                            output("$allUrls[$i] downloaded <br>");

                    }else{

                            output("Failed $allUrls[$i] downloaded <br>"); 

                    }

                }
    
                
    
}

// If it has Renders, proceed to download and store them in the local folder
if($hasRenders == 1){
    
    

                $urlDrawings = str_replace('photos-videos','renders',$url);

                $htmlDrawings = file_get_contents($urlDrawings);

                $allUrls = getContents($htmlDrawings, '<img class="lazy" src="', '?fit=crop&');

                var_dump($allUrls);

                $title = get_string_between($html, '<meta property="og:title" content="', '| Media'); 

                $title = htmlspecialchars($title);

                $title = str_replace('|','',$title);
                $title = str_replace('\\','',$title);
                $title = str_replace('/','',$title);
                $title = str_replace(';','',$title);
                $title = str_replace('&amp','',$title);
                $title = str_replace('amp','',$title);
                $title = str_replace(',','',$title);
                $title = str_replace('  ',' ',$title);
                $title = str_replace(' ','-',$title);

                echo $title;

                if (!file_exists("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/renders")) {
                    mkdir("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/renders", 0777, true);
                }
    
    
                for($i = 0; $i < count($allUrls); $i ++){
                    sleep(2);
                    $content = file_get_contents(str_replace('https://XXXXXXXX.com/thumbs', 'https://XXXXXXXX.s3.eu-central-1.amazonaws.com', $allUrls[$i]));
                    if(file_put_contents("projects/$category/".str_pad($index, 5, "0", STR_PAD_LEFT)."-$title/renders/".($i+1).".jpg", $content)){

                            output("$allUrls[$i] downloaded <br>");

                    }else{

                            output("Failed $allUrls[$i] downloaded <br>"); 

                    }

                }
    
}

$index ++;
}