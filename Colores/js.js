// 02.10.2021
// Tutorial

const rgb_r = document.getElementById('rgb_r');
const rgb_g = document.getElementById('rgb_g');
const rgb_b = document.getElementById('rgb_b');
const hex = document.getElementById('hex');
const cmyk_c = document.getElementById('cmyk_c');
const cmyk_m = document.getElementById('cmyk_m');
const cmyk_y = document.getElementById('cmyk_y');
const cmyk_k = document.getElementById('cmyk_k');

function generate(){
			// Valor aleatorio para R, G, B
            r = Math.round(Math.random() * 254);
            g = Math.round(Math.random() * 254);
            b = Math.round(Math.random() * 254);
			// Aplicar RGB aleatorio al fondo
            // document.body.style.backgroundColor = "rgb("+r+', '+g+', '+b+')';

			// Cambiar valores de los span
            rgb_r.innerText = r;
            rgb_g.innerText = g;
            rgb_b.innerText = b;

			// Convertir valores RGB a hexadecimal
            hex.innerText = (r.toString(16)+g.toString(16)+b.toString(16)).toUpperCase();

			

		
            
			
			// Aplicar gradiente al fondo
            grad =  'linear-gradient(#000000, #'+(r.toString(16)+g.toString(16)+b.toString(16))+')';
			console.log(grad);
            document.body.style.background = grad; 


			// Conversión a CMYK
            rp = r / 255;
            gp = g / 255;
            bp = b / 255;          
            k = Math.min(Math.min(255 - r, 255 - g), 255 - b);
            c = Math.round((255-r-k)/(255-k) *100);
            m = Math.round((255-g-k)/(255-k) *100);
            y = Math.round((255-b-k)/(255-k) *100);
            cmyk_c.innerText = c;
            cmyk_m.innerText = m;
            cmyk_y.innerText = y;
            cmyk_k.innerText = k;

        
        }

generate();