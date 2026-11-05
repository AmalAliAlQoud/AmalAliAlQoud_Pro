import 'dart:convert';
import 'package:http/http.dart' as http;
import 'ConectionString.dart';

class AuthService {
  // دالة لإرسال كود التحقق للإيميل
  Future<bool> sendVerificationCode(String email, String password) async {
    try {
      final response = await http.post(
        Uri.parse('${ConectionString.baseUrl}/Auth/Login'),
        headers: {'Content-Type': 'application/json'},
        body: jsonEncode({
          'email': email,       // تم التعديل ليتطابق مع الـ Backend
          'password': password,
        }),
      );

      print('Send Code Status: ${response.statusCode}');
      print('Send Code Body: ${response.body}');

      if (response.statusCode == 200) {
        return true; // السيرفر أرسل الكود بنجاح للإيميل
      }
    } catch (e) {
      print('Error sending code: $e');
    }
    return false;
  }

  // دالة للتحقق من صحة الرمز المكون من 4 أرقام
  Future<bool> verifyCode(String email, String code) async {
    try {
      final response = await http.post(
        Uri.parse('${ConectionString.baseUrl}/Auth/VerifyCode'),
        headers: {'Content-Type': 'application/json'},
        body: jsonEncode({
          'email': email,       // تم التعديل ليتطابق مع الـ Backend
          'code': code,
        }),
      );

      print('Verify Status: ${response.statusCode}');
      print('Verify Body: ${response.body}');

      if (response.statusCode == 200) {
        return true; // الكود صحيح وسمح له بالدخول
      }
    } catch (e) {
      print('Error verifying code: $e');
    }
    return false;
  }
}