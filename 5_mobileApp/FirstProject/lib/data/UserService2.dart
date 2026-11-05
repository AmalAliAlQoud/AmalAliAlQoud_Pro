import 'dart:convert';
import 'package:http/http.dart' as http;
import 'ConectionString.dart';

class UserService2 {
  // دالة تسجيل الدخول
  Future login(String email, String password) async {
    try {
      final response = await http.post(
        Uri.parse(ConectionString.login2),
        headers: {
          'Content-Type': 'application/json',
        },
        body: json.encode({
          'email': email,
          'password': password,
        }),
      );

      if (response.statusCode == 200) {
        return json.decode(response.body);
      } else if (response.statusCode == 400 || response.statusCode == 401) {
        return null;
      } else {
        throw Exception('Failed to login: ${response.statusCode}');
      }
    } catch (e) {
      print('Login Exception: $e');
      rethrow;
    }
  }

  // دالة إنشاء حساب جديد
  Future register(
      String name,
      String email,
      String username,
      String password,
      ) async {
    try {
      final response = await http.post(
        Uri.parse(ConectionString.register2),
        headers: {
          'Content-Type': 'application/json',
        },
        body: json.encode({
          'name': name,
          'email': email,
          'username': username,
          'password': password,
        }),
      );

      if (response.statusCode == 200) {
        return json.decode(response.body);
      } else if (response.statusCode == 409 || response.statusCode == 400) {
        return null;
      } else {
        throw Exception('Registration failed: ${response.body}');
      }
    } catch (e) {
      print('Register Exception: $e');
      rethrow;
    }
  }

  // دالة التحقق من الرمز
  Future verifyCode(String email, String code) async {
    try {
      final response = await http.post(
        Uri.parse('${ConectionString.baseUrl}/Auth/VerifyCode'),
        headers: {
          'Content-Type': 'application/json',
        },
        body: json.encode({
          'email': email,
          'code': code,
        }),
      );

      if (response.statusCode == 200) {
        return json.decode(response.body);
      } else {
        return null;
      }
    } catch (e) {
      print('Verify Code Exception: $e');
      rethrow;
    }
  }
}