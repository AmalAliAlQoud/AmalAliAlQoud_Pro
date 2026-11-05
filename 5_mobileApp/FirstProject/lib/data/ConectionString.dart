class ConectionString {
 // static const String baseUrl = 'https://wwvd6c44-7113.inc1.devtunnels.ms/api';
   static const String baseUrl = 'https://fvq5v6g7-7113.uks1.devtunnels.ms/api';

  static const String login = '$baseUrl/Users/Login';
  static const String register = '$baseUrl/Users/Register';

   // تعديله ليوافق الـ AuthController الذي اختبرناه في Swagger
   static const String login2 = '$baseUrl/Auth/Login';
   static const String register2 = '$baseUrl/Auth/Register';

  //static const String dashboardUrl = 'https://wwvd6c44-7195.inc1.devtunnels.ms/';
   static const String dashboardUrl = 'https://fvq5v6g7-7195.uks1.devtunnels.ms/';


  static const String doctors = '$baseUrl/Doctors/GetDoctors';
  static const String patients = '$baseUrl/Patients/GetPatients';
   static const String Specially = '$baseUrl/Specialty/GetSpecialty';
   static const String doctorsBySpecialty = '$baseUrl/Doctors/GetDoctorsBySpecialty';



}